using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VsScreenUI : MonoBehaviour
{
    [SerializeField] Button shopButton;
    [SerializeField] Button freeButton;
    [SerializeField] Button playButton;

    [SerializeField] Image enemy1Img;
    [SerializeField] Image enemy2Img;
    [SerializeField] Image enemy3Img;

    [SerializeField] Sprite[] enemySprite;
    private void Awake() {
        shopButton.onClick.AddListener(ShopButton);
        freeButton.onClick.AddListener(FreeButton);
        playButton.onClick.AddListener(CallStartGame);
    }
    private void OnEnable() {
        int value = CharacterStats.Instance.EnemyLevel;
        if (value == 0) value = 1;
        SetActiveButton(true);
        enemy1Img.sprite = enemySprite[value - 1];
        enemy1Img.SetNativeSize();
        if (value == 5 || value == 6) {
            enemy3Img.gameObject.SetActive(false);
            enemy2Img.gameObject.SetActive(true);
            if (value == 5) {
                enemy2Img.sprite = enemySprite[1];
                enemy2Img.SetNativeSize();
            } else {
                enemy2Img.sprite = enemySprite[2];
                enemy2Img.SetNativeSize();
            }
            
        }
        else if (value == 9) {
            enemy3Img.gameObject.SetActive(true);
            enemy3Img.sprite = enemySprite[2];
            enemy3Img.SetNativeSize();

            enemy2Img.gameObject.SetActive(true);
            enemy2Img.sprite = enemySprite[7];
            enemy2Img.SetNativeSize();
        }
        else {
           enemy3Img.gameObject.SetActive(false);
           enemy2Img.gameObject.SetActive(false);
        }
    }
    void CallStartGame() {
        SetActiveButton(false);
        RestartGameManager.Instance.ReStartObject();
        MainSoundManager.Instance.ChangeSound();
        SoundEffectManager.Instance.ActiveClickSound();
        PanelManager.Instance.SwitchActiveUI(GameUI.VsScreen, GameUI.PlayScreen);
        //FadeInOutManager.Instance.FadeOut();
    }
    void ShopButton() {
        SetActiveButton(false);
        SoundEffectManager.Instance.ActiveClickSound();
        PanelManager.Instance.SwitchActiveUI(GameUI.VsScreen, GameUI.Transform);
        FadeInOutManager.Instance.FadeOut();
    }
    void FreeButton() {
        SetActiveButton(false);
        SoundEffectManager.Instance.ActiveClickSound();
        PanelManager.Instance.SwitchActiveUI(GameUI.VsScreen, GameUI.FreeScreen);
        FadeInOutManager.Instance.FadeOut();
    }
    void SetActiveButton(bool state) {
        playButton.interactable = state;
        shopButton.interactable = state;
        freeButton.interactable = state;
    }
}
