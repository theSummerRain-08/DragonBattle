using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskGameOverUI : MonoBehaviour
{
    private void OnEnable() {
        StartCoroutine(ChangeToVictoryScreen());
    }

    IEnumerator ChangeToVictoryScreen() {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
        CharacterStats.Instance.ResetStats(Character.Player, true);
        RestartGameManager.Instance.UpdateEnemyStats(Results.Lose);
        CurrentCurrency.Instance.UpdateLevelCoin(Results.Lose);
        MainSoundManager.Instance.ChangeSound();
        PanelManager.Instance.SwitchActiveUI(GameUI.PlayScreen, GameUI.VictoryScreen);

    }

}
