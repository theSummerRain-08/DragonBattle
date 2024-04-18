using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private int playerLevel = 1;
    [SerializeField] private float playerAtk = 10f;
    [SerializeField] private float playerHp = 100f;
    [SerializeField] private float playerMana = 100f;

    [Header("Enemy Stat")]
    [SerializeField] private int enemyLevel = 1;
    [SerializeField] private float enemyAtk = 10f;
    [SerializeField] private float enemyHp = 100f;
    [SerializeField] private float enemyMana = 100f;

    [SerializeField] private float previousEnemyLevel = 1;
    
    public int PlayerLevel => playerLevel;
    public float PlayerAtk => playerAtk;
    public float PlayerHp => playerHp;
    public float PlayerMana => playerMana;

    public int EnemyLevel => enemyLevel;
    public float EnemyAtk => enemyAtk;
    public float EnemyHp => enemyHp;
    public float EnemyMana => enemyMana;
    public float PreviousEnemyLevel => previousEnemyLevel;
    [Header("Max Stat")]
    public float maxPlayerHp = 100f;
    public float maxPlayerMana = 100f;

    public float maxEnemyHp = 100f;
    public float maxEnemyMana = 100f;
    [Header("")]
    public float startCharacterAtk = 10f;
    public float startHp = 100f;

    public int maxPlayerLevel = GameConstants.levelCharacter.Length;
    private void OnEnable() {
        enemyLevel = PlayerPrefs.GetInt("EnemyLevel", 0);
        if (enemyLevel == 0) enemyLevel = 1;
        previousEnemyLevel = PlayerPrefs.GetInt("PreviousEnemyLevel", 0);
        if (previousEnemyLevel == 0) previousEnemyLevel = 1;
        UpdateEnemyStats();
    }
    public void TakeDamage(Character character, float damage) {
        if (character == Character.Player) { 
            playerHp -= damage;
        }

        if (character == Character.Enemy) {
            enemyHp-= damage;
        }
    }
    public void UpdateEnemyPreviousLevel() { 
        previousEnemyLevel = enemyLevel;
        PlayerPrefs.SetInt("PreviousEnemyLevel", enemyLevel);
        PlayerPrefs.Save();
    }
    //______________________mana_________________________________
    public void ManaToCastSkill(AttackType type) {
        playerMana -= GameConstants.ManaToCastSkill[(int)type];
    }
    //___________________________________________________________




    //___________________transform funcion_______________________
    public void TransformToNextLevel() {
        if (playerLevel == maxPlayerLevel)
            return;
        
        playerMana = maxPlayerMana;
        playerHp = maxPlayerHp;

        playerLevel++;
        if (playerLevel >= maxPlayerLevel) playerLevel = maxPlayerLevel;
        UpdatePlayerStats(Character.Player);
    }
    //__________________________________________________________





    //__________________update funcion_________________________
    public void UpdatePlayerStats(Character character) {
        if (character == Character.Player) {
            playerAtk = startCharacterAtk * playerLevel;
            playerHp = startHp * playerLevel;
            maxPlayerHp = startHp * playerLevel;
        }
    }
    public void UpdateEnemyStats(Results results) {
        if (results == Results.Win) {
            enemyLevel++;
            if (enemyLevel >= 10) enemyLevel = 10;
            PlayerPrefs.SetInt("EnemyLevel", enemyLevel);
            PlayerPrefs.Save();
            UpdateEnemyStats();
        }

        if (results == Results.Lose) {
            enemyLevel -= 2;
            PlayerPrefs.SetInt("EnemyLevel", enemyLevel);
            PlayerPrefs.Save();
            if (enemyLevel <= 1) enemyLevel = 1;
            UpdateEnemyStats();
        }
    }
    void UpdateEnemyStats() {
        enemyAtk = startCharacterAtk * enemyLevel;
        enemyHp = startHp * enemyLevel;
        maxEnemyHp = startHp * enemyLevel;
    }
    public void ResetStats(Character character, bool restartLevel) {
        if (character == Character.Player) {
            if (restartLevel) {
                playerLevel = 1;
                UpdatePlayerStats(character);
            }
            playerHp =  maxPlayerHp ;
            playerMana = maxPlayerMana;
        }

        if (character == Character.Enemy) {
            enemyHp = maxEnemyHp;
        }
    }
    //__________________________________________________________
    



    //___________________buff funcion__________________________
    public void PlusMana(float value) {
        playerMana += value;
    }
    public void BuffMana() {
        playerMana = maxPlayerMana;
    }
    public void BuffHp() {
        playerHp = maxPlayerHp;
    }
    //_________________________________________________________
}
