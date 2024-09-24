using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FreeScreenUI : MonoBehaviour
{
    [SerializeField] Button homeButton;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] Button coinButton;
    [SerializeField] GameObject luckyWheel;
    [SerializeField] Button spinButton;
    [SerializeField] Button likeButton;
    [SerializeField] Button adsButton;
    [SerializeField] GameObject videoAdsObj;
    private void Awake() {
        homeButton.onClick.AddListener(HomeButton);
        coinButton.onClick.AddListener(CoinFuncion);
        spinButton.onClick.AddListener(SpinFuncion);
        likeButton.onClick.AddListener(OpenWebsite);
        adsButton.onClick.AddListener(AdsFuncion);
    }


    private void OnEnable() {
        homeButton.interactable = true;
        coinButton.interactable = true;
        StartCoroutine(FadeInWait());
    }
    private void Update() {
        if (CurrentCurrency.Instance.CurrentGold <= 9999999) {
            goldText.text = CurrentCurrency.Instance.CurrentGold.ToString();
        } else goldText.text = "10 000 000+";
    }
    IEnumerator FadeInWait() {
        yield return new WaitForSeconds(GameConstants.TimeToChange);
        FadeInOutManager.Instance.FadeIn();
    }


    void HomeButton() {
        homeButton.interactable = false;
        PanelManager.Instance.SwitchActiveUI(GameUI.FreeScreen, GameUI.HomeScreen);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }
    void CoinFuncion() {
        coinButton.interactable = false;
        PanelManager.Instance.SwitchActiveUI(GameUI.FreeScreen, GameUI.CoinsScreen);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }
    void OpenWebsite() {
        Application.OpenURL("https://www.facebook.com/mysticgamestudio");
    }
    void SpinFuncion() { 
        luckyWheel.SetActive(true);
    }
    void AdsFuncion() {
        videoAdsObj.gameObject.SetActive(true);
    }
}
