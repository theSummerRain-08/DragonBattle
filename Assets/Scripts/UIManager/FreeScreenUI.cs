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
    private void Awake() {
        homeButton.onClick.AddListener(HomeButton);
    }


    private void OnEnable() {
        homeButton.interactable = true;
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
}
