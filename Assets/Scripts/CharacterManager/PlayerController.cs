using System.Collections;
using UnityEngine;
using static GameEnum;

public class PlayerController : CharacterController {
    [SerializeField] private LayerMask layerMask;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject bumpObject;
    public Transform spawnPosition;
    public static bool hasShield;
    private void Start() {
        characterState = CharacterState.Idle;
    }
    private void OnEnable() {
        characterSketon.UpdateSkin("0");
        hasShield = false;
        shield.SetActive(false);
    }

    void Update() {
        if (CharacterStats.Instance.PlayerHp <= 0) {
            Die();
        }
        if (hasShield) { shield.SetActive(true); }


        if (Input.touchCount > 0) {
            Move();
        }
        else
            hasTouched = false;

    }

    #region Move funcion

    private float moveSpeed = 15f;
    Touch currentTouch;
    bool hasTouched = false;
    protected override void Move() {
        if (characterState == CharacterState.Attack) return;

        Touch touch = Input.GetTouch(0);
        if (hasTouched) {
            if (touch.fingerId != currentTouch.fingerId) {
                return;
            }
        } else {
            currentTouch = touch;
            hasTouched = true;
        }

        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
        touchPosition.z = 0f;
        MoveCharacter(touchPosition);

    }
    private void MoveCharacter(Vector3 targetPosition) {
        if (Hit().collider == null) return;
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetPosition);
        transform.position += direction * Mathf.Min(distance, moveSpeed * Time.deltaTime);
    }
    private RaycastHit2D Hit() {
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask);
    }


    private void UpdatePosition(RaycastHit2D hit) {
        float lerfSpeed = 6f;
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = new Vector3(hit.point.x, hit.point.y);
        transform.position = Vector2.Lerp(currentPosition, newPosition, lerfSpeed * Time.deltaTime);
    }
    #endregion

    #region Attack Funcion
    public override void Attack(AttackType type) {
        if (characterState != CharacterState.Idle) return;
        if (CharacterStats.Instance.PlayerMana < GameConstants.ManaToCastSkill[(int)type]) return;
        //characterState = (type == AttackType.Skill3) ? CharacterState.Attack3 : CharacterState.Attack;
        switch (type) {
            case AttackType.NormalAttack:
                characterParticle.PlayHandParticle();
                break;
            case AttackType.Skill1:
                characterParticle.PlayKamehaEff();
                characterState = CharacterState.Attack;
                break;
            case AttackType.Skill2:
                characterParticle.PlayHandParticle();
                break;
            case AttackType.Skill3:
                characterParticle.PlayKameha2Eff();
                characterState = CharacterState.Attack;
                break;
            case AttackType.Skill4:
                characterParticle.PlayHandParticle();
                break;
        }

        if (type == AttackType.Skill3) {
            StartCoroutine(ChangeState());
        }

        characterAttack.attackType = type;

        characterSketon.SetAnimation(type);
        CharacterSound.Instance.SetAtkSound(type);
        CastSkillFuncion(type);
    }
    IEnumerator ChangeState() {
        yield return new WaitForSeconds(0.5f);
        characterState = CharacterState.Attack3;
    }

    void CastSkillFuncion(AttackType type) {
        CharacterStats.Instance.ManaToCastSkill(type);
        if (type == AttackType.NormalAttack) {
            StartCoroutine(CastNormalAttack(type, characterAttack.SkillPosition(type, transform.position), characterAttack.TimeToCastSkill(type)));
        } else {
            StartCoroutine(CastSkill(type, characterAttack.SkillPosition(type, transform.position), characterAttack.TimeToCastSkill(type)));
        }

    }
    IEnumerator CastSkill(AttackType type, Vector3 position, float timeToCastSkill) {
        yield return new WaitForSeconds(timeToCastSkill);

        characterAttack.UseSkill(type, position, Character.Player);
        characterParticle.StopHandParticle();
    }
    IEnumerator CastNormalAttack(AttackType type, Vector3 position, float timeToCastSkill) {
        yield return new WaitForSeconds(timeToCastSkill);
        characterAttack.UseNormalAttack(position);
        characterParticle.StopHandParticle();
    }


    #endregion

    #region Die and Ult Fucion
    protected override void Die() {
        CharacterSound.Instance.SetDieSound();
        this.gameObject.SetActive(false);
        bumpObject.transform.position = this.transform.position;
        bumpObject.SetActive(true);
    }

    public ParticleSystem lightningPar;
    public void UltimateSkill() {
        if (CharacterStats.Instance.PlayerLevel == GameConstants.levelCharacter.Length) return;
        if (characterState != CharacterState.Idle) return;

        lightningPar.Play();
        characterParticle.PlayHandParticle();
        CharacterSound.Instance.SetTransformSound();
        UpdateStatsVakue();
        UpdateSkin();
        StartCoroutine(SetActiveShield());
    }
    void UpdateStatsVakue() {
        CharacterStats.Instance.TransformToNextLevel();
        characterState = CharacterState.Transform;
        characterSketon.SetTransformAnim(1f);
    }
    void UpdateSkin() {
        float skinValue = CharacterStats.Instance.PlayerLevel - 1;
        characterSketon.UpdateSkin(skinValue.ToString());
    }
    IEnumerator SetActiveShield() {
        if (!hasShield) {
            shield.SetActive(true);
            yield return new WaitForSeconds(1);
            shield.SetActive(false);
        }
        characterState = CharacterState.Idle;
    }
    #endregion

    #region Take dmg
    public GameObject particleSystemPrefab;
    public ParticleSystem healingPar;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Buff")) {
            SoundEffectManager.Instance.ActiveCollectItemSound(Item.Buff);
            healingPar.Play();
        }
        if (collision.CompareTag("Shield")) {
            SoundEffectManager.Instance.ActiveCollectItemSound(Item.Buff);
        }
        if (collision.CompareTag("Coin")) {
            SoundEffectManager.Instance.ActiveCollectItemSound(Item.Coin);
        }
        if (collision.CompareTag("EnemySkill")) {
            Vector3 collisionPoint = collision.transform.position;
            ObjectPooling.Instance.SpawnObject(particleSystemPrefab, collisionPoint);
        }

    }
    #endregion
    private void OnDisable() {
        characterSketon.SetIdleAnim();
        characterParticle.DisplayKamehaEff();
        characterParticle.StopHandParticle();
        characterParticle.DisplayKameha2Eff();
        if (CharacterStats.Instance.PlayerHp > 0) {
            CharacterStats.Instance.ResetStats(Character.Player, true);
            this.transform.position = spawnPosition.position;
        }
    }
}
