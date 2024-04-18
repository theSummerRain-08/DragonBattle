using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameManager : MonoBehaviour
{
    public static RestartGameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private GameObject victoryMask;
    [SerializeField] private GameObject loseMask;

    [SerializeField] private Transform playerSopawnPosition;
    [SerializeField] private Transform enemySpawnPosition;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject enemyObj;

    public void ReStartObject() {

        CharacterStats.Instance.ResetStats(Character.Player, false); 
        CharacterStats.Instance.ResetStats(Character.Enemy, false);
        
        if (victoryMask.activeSelf) {
            victoryMask.SetActive(false);
        }
        if (loseMask.activeSelf) {
            loseMask.SetActive(false);
        }
        //MainSoundManager.Instance.ChangeSound();
        playerObj.transform.position = playerSopawnPosition.transform.position;
        if (!enemyObj.activeSelf) {
            enemyObj.SetActive(true);
        }
        enemyObj.transform.position = enemySpawnPosition.transform.position;
        if (!playerObj.activeSelf) {
            playerObj.SetActive(true);
        }
    }

    public void UpdateEnemyStats(Results result) {
        CharacterStats.Instance.UpdateEnemyPreviousLevel();
        CharacterStats.Instance.UpdateEnemyStats(result);
    }
}
