using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill1Obj : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float dmgScale;
    public bool isUltSkill;
    private void OnEnable() {
        EnemySoundManager.Instance.SetSkillSound();
    }
    private void Update() {
        Move();
        if (GameUltis.ExitScreen(this.transform.position) || CharacterStats.Instance.PlayerHp <= 0 || CharacterStats.Instance.EnemyHp <= 0) {
            DeSpawn();
        }
    }

    public void Move() {
        float newPositionX = transform.position.x - speed * Time.deltaTime;
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }

    public void DeSpawn() {
        ObjectPooling.Instance.DeSpawn(this.gameObject);
    }

    public GameObject particleSystemPrefab;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            DeSpawn();
            CharacterStats.Instance.TakeDamage(Character.Player,
                CharacterStats.Instance.EnemyAtk * dmgScale);
            if (GameEnum.characterState != CharacterState.Idle) return;
            CharacterSound.Instance.SetTakeDmgSound();
        }
        if (collision.CompareTag("UltSkill")) {
            Vector3 collisionPoint = collision.transform.position;
            ObjectPooling.Instance.SpawnObject(particleSystemPrefab, collisionPoint);
            DeSpawn();
        }
        if (collision.CompareTag("Shield") ) {
            DeSpawn();
        }
        if (collision.CompareTag("PlayerSkill") && !isUltSkill) {
            Vector3 collisionPoint = collision.transform.position;
            ObjectPooling.Instance.SpawnObject(particleSystemPrefab, collisionPoint);
            DeSpawn();
        }


    }
    
}
