using UnityEngine;

public class N_AttackObj1 : NormalAttackObjj {

    void Update() {
        if (GameUltis.ExitScreen(this.transform.position)) {
            DeSpawn();
        }

        Move();
        
    }
    public GameObject particleSystemPrefab;
    public override void Move() {
        float heightParabol = 2f + 0.5f * randomNum[0];
        float angel = 20 + 10 * randomNum[0];
        float speed = 12f;
        GoParabol(heightParabol, speed, angel, -angel);
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
