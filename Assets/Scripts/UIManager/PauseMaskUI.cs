using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMaskUI : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Button homeButton;
    private void Awake() {
        continueButton.onClick.AddListener(ContinueGame);
        homeButton.onClick.AddListener(HomeButton);
    }
    void ContinueGame() { 
        this.gameObject.SetActive(false);
        SoundEffectManager.Instance.ActiveClickSound();
        Time.timeScale = 1.0f;
    }
    void HomeButton() {
        PanelManager.Instance.SwitchActiveUI(GameUI.PlayScreen, GameUI.HomeScreen);
        SoundEffectManager.Instance.ActiveClickSound();
        FadeInOutManager.Instance.FadeOut();
        MainSoundManager.Instance.ChangeSound();
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
