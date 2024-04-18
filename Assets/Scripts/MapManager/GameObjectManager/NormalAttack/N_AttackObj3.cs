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

    public override void Move() {
        float heightParabol = 0f;
        float speed = 10f;
        GoParabol(heightParabol, speed, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            DeSpawn();
            CharacterStats.Instance.TakeDamage(Character.Enemy, dmgPerUnit);
        }
    }

}
