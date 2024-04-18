using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChooseUI : MonoBehaviour
{
    [SerializeField] Button homeButton;
    [SerializeField] Button freeButton;
    [SerializeField] Button coinsButton;


    [SerializeField] Button[] selectButton;
    [SerializeField] Button[] transformButton;

    [SerializeField] TextMeshProUGUI[] selectText;

    [SerializeField] TextMeshProUGUI goldText;
    private void Awake() {
        homeButton.onClick.AddListener(HomeButton);
        freeButton.onClick.AddListener(FreeButton);
        coinsButton.onClick.AddListener(CoinsButton);

        for (int i = 0; i < selectButton.Length; i++) {
            int index = i;
            selectButton[index].onClick.AddListener(() => ChangeSelectUI(index));
            transformButton[index].onClick.AddListener(() => ChangeSelectUI(index));
        }

    }
  
    private void OnEnable() {
        if (CurrentCurrency.Instance.CurrentGold <= 9999999) {
            goldText.text = CurrentCurrency.Instance.CurrentGold.ToString();
        } else goldText.text = "10 000 000+";
        SetActiveButton(true);
        selectText[(int)CharacterSelectManager.characterToSelect].text = "SELECTED";
        selectButton[(int)CharacterSelectManager.characterToSelect].interactable = false;
        StartCoroutine(FadeInWait());
    }
    IEnumerator FadeInWait() {
        yield return new WaitForSeconds(GameConstants.TimeToChange);
        FadeInOutManager.Instance.FadeIn();
    }

    void HomeButton() {
        SetActiveButton(false);
        PanelManager.Instance.SwitchActiveUI(GameUI.CharacterChoose, GameUI.HomeScreen);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }
    void FreeButton() {
        SetActiveButton(false);
        PanelManager.Instance.SwitchActiveUI(GameUI.CharacterChoose, GameUI.FreeScreen);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }
    void CoinsButton() {
        SetActiveButton(false);
        PanelManager.Instance.SwitchActiveUI(GameUI.CharacterChoose, GameUI.CoinsScreen);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }

    void ChangeSelectUI(int index) {
        SoundEffectManager.Instance.ActiveClickSound();
        for (int i = 0; i < selectText.Length; i++) {
            if (i == index) {
                selectText[i].text = "SELECTED";
                CharacterSelectManager.characterToSelect = (CharacterToSelect)index;
                selectButton[i].interactable = false;
            } else {
                selectText[i].text = "SELECT";
                selectButton[i].interactable = true;
            }
        }
        SetActiveButton(false);
        PanelManager.Instance.SwitchActiveUI(GameUI.CharacterChoose, GameUI.Transform);
        FadeInOutManager.Instance.FadeOut();
    }
    void SetActiveButton(bool state) {
        homeButton.interactable = state;
        freeButton.interactable = state;
        coinsButton.interactable = state;
        for (int i = 0; i < selectButton.Length; i++) {
            selectButton[i].interactable = state;
            transformButton[i].interactable = state;
        }
    }
}
