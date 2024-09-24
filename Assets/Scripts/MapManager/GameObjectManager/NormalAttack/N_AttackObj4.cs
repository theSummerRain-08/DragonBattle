using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_AttackObj4 : NormalAttackObjj
{
    void Update() {
        if (GameUltis.ExitScreen(this.transform.position)) {
            DeSpawn();
        }

        Move();
    }
    public GameObject particleSystemPrefab;
    public override void Move() {
        float heightParabol = -1f;
        float speed = 11f;
        GoParabol(heightParabol, speed, -10f, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            DeSpawn();
            CharacterStats.Instance.TakeDamage(Character.Enemy, dmgPerUnit);
        }
        if (collision.CompareTag("EnemySkill")) {
            Vector3 collisionPoint = collision.transform.position;
            ObjectPooling.Instance.SpawnObject(particleSystemPrefab, collisionPoint);
            DeSpawn();
        }
    }
}
