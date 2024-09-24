using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class N_AttackObj3 : NormalAttackObjj
{
    void Update() {
        if (GameUltis.ExitScreen(this.transform.position)) {
            DeSpawn();
        }

        Move();
    }
    public GameObject particleSystemPrefab;
    public override void Move() {
        float heightParabol = 0f;
        float speed = 11f;
        GoParabol(heightParabol, speed, 0, 0);
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
