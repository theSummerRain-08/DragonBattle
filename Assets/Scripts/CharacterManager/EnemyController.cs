using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : CharacterController {
    [SerializeField] SkeletonDataAsset[] newSkeletonDataAsset;
    private Vector3 targetPosition;
    private bool isMoving = false;
    [SerializeField] GameObject bumpObject;
    public float delayTime;
    public float enemySpeed = 20f;
    [SerializeField] Transform enemyFightPos;
    [SerializeField] Transform enemySpawnPos;
    private void OnEnable() {
        delayTime = GameConstants.enemyDelayTime[CharacterStats.Instance.EnemyLevel - 1];  

        //if (CharacterStats.Instance.EnemyLevel == 1) return;
        StartCoroutine(ChangeSkin());

    }
    IEnumerator ChangeSkin() { 
        yield return new WaitForSeconds(0.5f);
        characterSketon.ChangeEnemySkeletonData(newSkeletonDataAsset[CharacterStats.Instance.EnemyLevel - 1]);
        Move();
    }
    private void Start() {

    }
    private void Update() {
        if (CharacterStats.Instance.EnemyHp <= 0) {
            Die();
        }
    }
    protected override void Move() {
        StartCoroutine(MoveCharacterEvery5Seconds());
    }

    private IEnumerator MoveCharacterEvery5Seconds() {
        while (true) {
            
            yield return new WaitForSeconds(delayTime);
            // Chọn tọa độ x và y ngẫu nhiên
            float randomX = GameUltis.RandomIntNumber(0, 7);
            float randomY = GameUltis.RandomIntNumber(-2, 3);
            targetPosition = new Vector3(randomX, randomY, transform.position.z);

            StartCoroutine(MoveCharacter(targetPosition));
        }
    }

    private IEnumerator MoveCharacter(Vector3 target) {
        if (isMoving)
            yield break;

        isMoving = true;

        float journeyLength = Vector3.Distance(transform.position, target);
        float journeyTime = journeyLength / enemySpeed;


        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;
        while (elapsedTime < journeyTime) {
            transform.position = Vector3.Lerp(startingPos, target, (elapsedTime / journeyTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;

        int type = GameUltis.RandomIntNumber(1, 3); 
        Attack((AttackType)type);
    }

    public override void Attack(AttackType type) {
        characterSketon.SetEnemyAtkAnim(1.5f, type);
        StartCoroutine(CastSkill(type, EnemySkillPosition(type, transform.position), TimeDelaySkill(type)));
    }
    public Vector3 EnemySkillPosition(AttackType type, Vector3 position) {
        float skillPosiontionY = 0f;
        float skillPositionX = 0f;
        switch (type) {
            case AttackType.Skill1:
                skillPosiontionY = 1.5f;
                skillPositionX = -1.5f;
                break;
            case AttackType.Skill2:
                skillPosiontionY = 0.8f;
                skillPositionX = -1.5f;
                break;
            case AttackType.Skill3:
                skillPosiontionY = 0.8f;
                skillPositionX = -1.5f;
                break;
        }
        return new Vector3(position.x + skillPositionX, position.y + skillPosiontionY, position.z);
    }

    public float TimeDelaySkill(AttackType type) {
        float enemyLv = CharacterStats.Instance.EnemyLevel;
        if (enemyLv == 1 || enemyLv == 3 || enemyLv == 6 || enemyLv == 7) {
            float[] value = new float[4] { 00000, 0.8f, 0.8f, 0.8f };
            return value[(int)type];
        }
        else if (enemyLv == 2 || enemyLv == 4 || enemyLv == 5  || enemyLv == 9 || enemyLv == 8 || enemyLv == 10) {
            float[] value = new float[4] { 00000, 1.2f, 1.4f, 1.4f };
            return value[(int)type];
        }
        else return 0f;
    }
    IEnumerator CastSkill(AttackType type, Vector3 position, float timeToCastSkill) {
        yield return new WaitForSeconds(timeToCastSkill);
        characterAttack.UseSkill(type, position, Character.Enemy);
        isMoving = false;
    }
    protected override void Die() {
        EnemySoundManager.Instance.SetDieSound();
        this.gameObject.SetActive(false);
        bumpObject.transform.position = this.transform.position;
        bumpObject.SetActive(true);
    }

    public GameObject particleSystemPrefab;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("PlayerSkill") || collision.CompareTag("NormalAttack")|| collision.CompareTag("UltSkill")) {
            EnemySoundManager.Instance.SetTakeDmgSound();
            Vector3 collisionPoint = collision.transform.position;
            ObjectPooling.Instance.SpawnObject(particleSystemPrefab, collisionPoint);
        }

    }
    private void OnDisable() {
        isMoving = false;
        characterSketon.SetEnemyIdleAnim();
    }
}
