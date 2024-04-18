using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill1Obj : MonoBehaviour
{
    private float speed = 15f;
    private void OnEnable() {
        EnemySoundManager.Instance.SetSkillSound();
    }
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
            if (GameEnum.characterState != CharacterState.Idle) return;
            CharacterSound.Instance.SetTakeDmgSound();
        }
        if (collision.CompareTag("Shield") || collision.CompareTag("PlayerSkill")) {
            DeSpawn();
        }

    }
    
}
