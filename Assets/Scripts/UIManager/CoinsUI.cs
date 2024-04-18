using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CoinsUI : MonoBehaviour
{
    [SerializeField] Button shopButton;
    [SerializeField] TextMeshProUGUI goldText;

    private void Awake() {
        shopButton.onClick.AddListener(ShopButton);
    }
    private void OnEnable() {
        shopButton.interactable = true;
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
    void ShopButton() {
        shopButton.interactable = false;
        PanelManager.Instance.SwitchActiveUI(GameUI.CoinsScreen, GameUI.Transform);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }
}
