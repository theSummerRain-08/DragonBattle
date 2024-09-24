﻿using Unity.VisualScripting;
using UnityEngine;

public class Skill2Obj : MonoBehaviour, ISkillObj 
{
    private void OnEnable() {
        SoundEffectManager.Instance.ActiveSkillSound(2);
    }
    private float speed = 25f;
    private void Update() {
        Move();
        if (GameUltis.ExitScreen(this.transform.position)) { 
            DeSpawn();
        }
    }

    public void Move() {
        float newPositionX = transform.position.x + speed * Time.deltaTime;
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }

    public void DeSpawn() {
        ObjectPooling.Instance.DeSpawn(this.gameObject);
    }

    public GameObject particleSystemPrefab;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            DeSpawn();
            CharacterStats.Instance.TakeDamage(Character.Enemy, 
                CharacterStats.Instance.PlayerAtk * GameConstants.dmgScale[(int)AttackType.Skill2]);
        }
        if (collision.CompareTag("EnemySkill")) {
            Vector3 collisionPoint = collision.transform.position;
            ObjectPooling.Instance.SpawnObject(particleSystemPrefab, collisionPoint);
            DeSpawn();
        }
    }
}
