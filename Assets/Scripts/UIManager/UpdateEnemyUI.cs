using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateEnemyUI : MonoBehaviour
{
    [SerializeField] private Image enemyAvt;
    [SerializeField] private Sprite[] enemySprite;
    [SerializeField] private Sprite[] enemySpriteState2;
    private void Update() {
        if (CharacterStats.Instance.EnemyLevel == 5 && CharacterSelectManager.enemyState == 2) {
            enemyAvt.sprite = enemySpriteState2[0];
            enemyAvt.SetNativeSize();
            return;
        }
        if (CharacterStats.Instance.EnemyLevel == 6 && CharacterSelectManager.enemyState == 2) {
            enemyAvt.sprite = enemySpriteState2[1];
            enemyAvt.SetNativeSize();
            return;
        }
        if (CharacterStats.Instance.EnemyLevel == 9 && CharacterSelectManager.enemyState == 2) {
            enemyAvt.sprite = enemySpriteState2[2];
            enemyAvt.SetNativeSize();
            return;
        }
        if (CharacterStats.Instance.EnemyLevel == 9 && CharacterSelectManager.enemyState == 3) {
            enemyAvt.sprite = enemySpriteState2[3];
            enemyAvt.SetNativeSize();
            return;
        }
        enemyAvt.sprite = enemySprite[CharacterStats.Instance.EnemyLevel - 1];
        enemyAvt.SetNativeSize();
    }
}
