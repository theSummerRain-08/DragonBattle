using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskGamePlayUI : MonoBehaviour
{
    private void OnEnable() {
        StartCoroutine(ChangeToVictoryScreen());
    }

    IEnumerator ChangeToVictoryScreen() {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
        CharacterStats.Instance.ResetStats(Character.Player, true);

        RestartGameManager.Instance.UpdateEnemyStats(Results.Win);
        CurrentCurrency.Instance.UpdateLevelCoin(Results.Win);
        MainSoundManager.Instance.ChangeSound();
        PanelManager.Instance.SwitchActiveUI(GameUI.PlayScreen, GameUI.VictoryScreen);

    }
}
