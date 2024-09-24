using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class N_AttackObj2 : NormalAttackObjj
{
    void Update() {
        if (GameUltis.ExitScreen(this.transform.position)) {
            DeSpawn();
        }

        Move();
    }

    public override void Move() {
        float heightParabol =  0.5f + 0.5f * randomNum[1];
        float angel = 5 + 5 * randomNum[1];
        float speed = 12f;
        GoParabol(heightParabol, speed, 15f, -15f);
    }
    public GameObject particleSystemPrefab;
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
