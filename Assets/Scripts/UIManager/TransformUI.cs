using AirFishLab.ScrollingList.Demo;
using AirFishLab.ScrollingList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TransformUI : MonoBehaviour
{
    [SerializeField] Button playButton;

    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI playerNameText;
    private void Awake() {
        playButton.onClick.AddListener(PlayButton);

    }
    private void OnEnable() {
        playerNameText.text = GameConstants.playerName[(int)CharacterSelectManager.characterToSelect];
        playButton.interactable = true;
        StartCoroutine(FadeInWait());
       
    }
    private void Update() {
        if (CurrentCurrency.Instance.CurrentGold <= 9999999) {
            goldText.text = CurrentCurrency.Instance.CurrentGold.ToString();
        } else goldText.text = "10 000 000+";
        playButton.gameObject.SetActive(CircleChooseCharUI.charSelectNum != 3);
    }
    IEnumerator FadeInWait() {
        yield return new WaitForSeconds(GameConstants.TimeToChange);
        FadeInOutManager.Instance.FadeIn();
    }
    void PlayButton() {
        playButton.interactable = false;
        switch (CircleChooseCharUI.charSelectNum) {
            case 0:
                CharacterSelectManager.characterToSelect = CharacterToSelect.Goku;
                break;
            case 1:
                CharacterSelectManager.characterToSelect = CharacterToSelect.Vegeta;
                break;
            case 2:
                CharacterSelectManager.characterToSelect = CharacterToSelect.Trunk;
                break;
            case 3:
                CharacterSelectManager.characterToSelect = CharacterToSelect.Goku;
                break;
            case 4:
                CharacterSelectManager.characterToSelect = CharacterToSelect.Gohan;
                break;
        }
        PanelManager.Instance.SwitchActiveUI(GameUI.Transform, GameUI.ReadyScreen);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
    }


}
