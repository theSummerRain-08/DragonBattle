using UnityEngine;

public class Skill1Obj : MonoBehaviour, ISkillObj {
    Vector3 startPosition = Vector3.zero;
    GameObject enemyObject = null;
    private void Awake() {
        
    }
    private void OnEnable() {
        startPosition = transform.position;
    }

    // max speed = 1f
    private float speed = 30f;

    private void Update() {
        Move();
        if (GameUltis.ExitScreen(this.transform.position)) {
            DeSpawn();
        }
    }

    public void Move() {
        float step = speed * Time.deltaTime;
        if (enemyObject == null) {
            enemyObject = GameObject.Find("Enemy"); // Update enemy position if null
            if (enemyObject == null) return; // If still null, exit Move() function
        }
        transform.position = Vector3.MoveTowards(transform.position, enemyObject.transform.position, step);
    }

    public void DeSpawn() {
        ObjectPooling.Instance.DeSpawn(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            DeSpawn();
            CharacterStats.Instance.TakeDamage(Character.Enemy,
                CharacterStats.Instance.PlayerAtk * GameConstants.dmgScale[(int)AttackType.Skill1]);
        }
        
    }

    private void OnDisable() {
        SoundEffectManager.Instance.ActiveSkillSound(1);
    }
}
