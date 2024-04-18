using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill2Obj : MonoBehaviour
{
    private float speed = 15f;
    private void Update() {
        Move();
        if (GameUltis.ExitScreen(this.transform.position)) {
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


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            DeSpawn();
            CharacterStats.Instance.TakeDamage(Character.Player,
                CharacterStats.Instance.EnemyAtk);
        }
    }
}
