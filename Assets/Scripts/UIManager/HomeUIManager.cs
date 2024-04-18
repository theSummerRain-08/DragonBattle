using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button freeButton;
    [SerializeField] Button shopButton;
    [SerializeField] Button coinsButton;

    [SerializeField] Button Sbutton;
    [SerializeField] Button settingButton;
    [SerializeField] GameObject settingPopUp;
    [SerializeField] Button wheelButton;
    [SerializeField] GameObject wheelPopup;
    private void Awake() {
        playButton.onClick.AddListener(PlayButton);
        shopButton.onClick.AddListener(ShopButton);
        freeButton.onClick.AddListener(FreeButton);
        coinsButton.onClick.AddListener(CoinButton);

        settingButton.onClick.AddListener(SetActivePopUp);
        wheelButton.onClick.AddListener(SetActiveWheel);
    }

    private void OnEnable() {
        SetActiveButton(true);
        StartCoroutine(FadeInWait());
    }
    IEnumerator FadeInWait() {
        yield return new WaitForSeconds(GameConstants.TimeToChange);
        FadeInOutManager.Instance.FadeIn();
    }
    void SetActivePopUp() { 
        settingPopUp.SetActive(true);
        SoundEffectManager.Instance.ActiveClickSound(); 
    }
    void SetActiveWheel() { 
        wheelPopup.SetActive(true);
        SoundEffectManager.Instance.ActiveClickSound();
    }
    void PlayButton() {
        SetActiveButton(false);
        PanelManager.Instance.SwitchActiveUI(GameUI.HomeScreen, GameUI.ReadyScreen);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }

    void ShopButton() {
        SetActiveButton(false);
        PanelManager.Instance.SwitchActiveUI(GameUI.HomeScreen, GameUI.Transform);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }
    void FreeButton() {
        SetActiveButton(false);
        PanelManager.Instance.SwitchActiveUI(GameUI.HomeScreen, GameUI.FreeScreen);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }
    void CoinButton() {
        SetActiveButton(false);
        PanelManager.Instance.SwitchActiveUI(GameUI.HomeScreen, GameUI.CoinsScreen);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }
    void SetActiveButton(bool state) {
        playButton.interactable = state;
        shopButton.interactable = state;
        freeButton.interactable = state;
        coinsButton.interactable = state;
    }
}
