using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemyStateManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyObj;
    [SerializeField] private Transform enemySpawnPosition;
    private void OnEnable() {
        StartCoroutine(UpdateNextState());
    }
    IEnumerator UpdateNextState() {
        yield return new WaitForSeconds (1f);
        enemyObj.transform.position = enemySpawnPosition.transform.position;
        if (!enemyObj.activeSelf) {
            enemyObj.SetActive(true);
        }
        CharacterStats.Instance.ResetStats(Character.Enemy, false);
        yield return new WaitForSeconds(0.5f);
        CharacterSelectManager.enemyState++;
        if (CharacterSelectManager.enemyState >= 3) CharacterSelectManager.enemyState = 3;
        this.gameObject.SetActive(false);
    }
}
