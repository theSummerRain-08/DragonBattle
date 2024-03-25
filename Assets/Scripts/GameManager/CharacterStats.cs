using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Header("Player Stat")]
    [SerializeField] private float playerAtk = 10f;
    [SerializeField] private float playerHp = 100f;
    [SerializeField] private float playerDef = 0f;

    private float currentGold = 2000f;

    [Header("Enemy Stat")]
    [SerializeField] private float enemyAtk;
    [SerializeField] private float enemyHp;
    [SerializeField] private float enemyDef;

    public float PlayerAtk => playerAtk;
    public float PlayerHp => playerHp;
    public float PlayerDef => playerDef;

    public float CurrentGold => currentGold;
    public float EnemyAtk => enemyAtk;
    public float EnemyHp => enemyHp;
    public float EnemyDef => enemyDef;


}
