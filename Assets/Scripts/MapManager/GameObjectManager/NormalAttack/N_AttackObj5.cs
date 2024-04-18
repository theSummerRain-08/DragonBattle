using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_AttackObj5 : NormalAttackObjj
{
    void Update() {
        if (GameUltis.ExitScreen(this.transform.position)) {
            DeSpawn();
        }

        Move();
    }


    public override void Move() {
        float heightParabol = -2.5f;
        float speed = 9.5f;
        GoParabol(heightParabol, speed, -20f, 20f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            DeSpawn();
            CharacterStats.Instance.TakeDamage(Character.Enemy, dmgPerUnit);
        }
    }
}
