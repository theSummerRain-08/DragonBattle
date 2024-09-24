using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3Obj : MonoBehaviour, ISkillObj {
    Vector3 startPosition = Vector3.zero;
    GameObject enemyObject = null;
    private float timeToMove = 3;
    private SpriteRenderer spriteRenderer = null;
    private float scaleValue = 11f;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable() {
        spriteRenderer.enabled = false;
        SoundEffectManager.Instance.ActiveSkillSound(3);
        enemyObject = GameObject.Find("Enemy");
        startPosition = transform.position;
        StartCoroutine(ScaleOverTime());
    }
    private float speed = 30f;
    private bool canMove = false;

    private void Update() {
        if (!canMove) return;
        if (enemyObject == null) return;
        Move();
        if (GameUltis.ExitScreen(this.transform.position)) {
            DeSpawn();
        }
    }
    public void Move() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, enemyObject.transform.position, step);
    }
    public void DeSpawn() {
        ObjectPooling.Instance.DeSpawn(this.gameObject);
    }

    IEnumerator ScaleOverTime() {
        yield return new WaitForSeconds(timeToMove);
        this.transform.localScale *= scaleValue;
        spriteRenderer.enabled = true;
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if ((collision.CompareTag("Enemy") && canMove)|| this.transform.position == enemyObject.transform.position) {
            DeSpawn();
            CharacterStats.Instance.TakeDamage(Character.Enemy,
                CharacterStats.Instance.PlayerAtk * GameConstants.dmgScale[(int)AttackType.Skill3]);
        }
    }

    private void OnDisable() {
        SoundEffectManager.Instance.ActiveSkillSound(1);
        this.transform.localScale /= scaleValue;
        spriteRenderer.enabled = false;
        canMove = false;
    }
}
