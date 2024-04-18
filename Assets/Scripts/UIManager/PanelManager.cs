using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {
    [SerializeField] GameObject playScreenUI;
    [SerializeField] GameObject homeScreenUI;
    [SerializeField] GameObject characterChooseUI;
    [SerializeField] GameObject transformUI;
    [SerializeField] GameObject readyUI;
    [SerializeField] GameObject vsScreenUI;
    [SerializeField] GameObject coinsScreenUI;
    [SerializeField] GameObject freeScreenUI;
    [SerializeField] GameObject victoryScreenUI;

    public static PanelManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void SwitchActiveUI(GameUI screenDisable, GameUI screenEnable) {
        StartCoroutine(DelayCallSwitch(screenDisable, screenEnable));
    }

    IEnumerator DelayCallSwitch(GameUI screenDisable, GameUI screenEnable) {
        yield return new WaitForSeconds(0.7f);
        switch (screenDisable) {
            case GameUI.PlayScreen:
                playScreenUI.SetActive(false);
                break;
            case GameUI.HomeScreen:
                homeScreenUI.SetActive(false);
                break;
            case GameUI.CharacterChoose:
                characterChooseUI.SetActive(false);
                break;
            case GameUI.Transform:
                transformUI.SetActive(false);
                break;
            case GameUI.ReadyScreen:
                readyUI.SetActive(false);
                break;
            case GameUI.VsScreen:
                vsScreenUI.SetActive(false);
                break;
            case GameUI.FreeScreen:
                freeScreenUI.SetActive(false);
                break;
            case GameUI.CoinsScreen:
                coinsScreenUI.SetActive(false);
                break;
            case GameUI.VictoryScreen:
                victoryScreenUI.SetActive(false);
                break;
        }
        
        switch (screenEnable) {
            case GameUI.PlayScreen:
                playScreenUI.SetActive(true);
                break;
            case GameUI.HomeScreen:
                homeScreenUI.SetActive(true);
                break;
            case GameUI.CharacterChoose:
                characterChooseUI.SetActive(true);
                break;
            case GameUI.Transform:
                transformUI.SetActive(true);
                break;
            case GameUI.ReadyScreen:
                readyUI.SetActive(true);
                break;
            case GameUI.VsScreen:
                vsScreenUI.SetActive(true);
                break;
            case GameUI.FreeScreen:
                freeScreenUI.SetActive(true);
                break;
            case GameUI.CoinsScreen:
                coinsScreenUI.SetActive(true);
                break;
            case GameUI.VictoryScreen:
                victoryScreenUI.SetActive(true);
                break;
        }
    }
}
