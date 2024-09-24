using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenUI : MonoBehaviour
{
    [SerializeField] Button homeButton;
    [SerializeField] Button shopButton;
    [SerializeField] Button playButton;

    [SerializeField] Image playerUI;
    [SerializeField] Sprite[] playerSprite;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI levelBonusText;
    [SerializeField] TextMeshProUGUI earnCoinText;
    [SerializeField] TextMeshProUGUI myCoinText;

    public static float earnCoin;
    private void Awake() {
        homeButton.onClick.AddListener(HomeButton);
        shopButton.onClick.AddListener(ShopButton);
        playButton.onClick.AddListener(PlayButton);


    }
    private void OnEnable() {
        playerUI.sprite = playerSprite[(int)CharacterSelectManager.characterToSelect];
        SoundEffectManager.Instance.EarnCoinSound();  
        scoreText.text = CurrentCurrency.Instance.levelCoin.ToString();
        earnCoin = CurrentCurrency.Instance.CurrentGold - CurrentCurrency.Instance.previousGold;
        if (CurrentCurrency.Instance.CurrentGold <= 9999999) {
            myCoinText.text = CurrentCurrency.Instance.CurrentGold.ToString();
        } else myCoinText.text = "10 000 000+";
        SetActiveButton(true);
        StartCoroutine(ChangeLevelCoinText());
        StartCoroutine(ChangeEarnCoinText());
        if (CurrentCurrency.Instance.CurrentGold > 9999999) return;
        StartCoroutine(ChangeMyCoinText());
    }
    void HomeButton() {
        SetActiveButton(false);
        SoundEffectManager.Instance.ActiveClickSound();
        PanelManager.Instance.SwitchActiveUI(GameUI.VictoryScreen, GameUI.HomeScreen);
        FadeInOutManager.Instance.FadeOut();
    }
    void ShopButton() {
        SetActiveButton(false);
        SoundEffectManager.Instance.ActiveClickSound();
        PanelManager.Instance.SwitchActiveUI(GameUI.VictoryScreen, GameUI.Transform);
        FadeInOutManager.Instance.FadeOut();
    }
    void PlayButton() {
        SetActiveButton(false);
        SoundEffectManager.Instance.ActiveClickSound();
        PanelManager.Instance.SwitchActiveUI(GameUI.VictoryScreen, GameUI.ReadyScreen);
        FadeInOutManager.Instance.FadeOut();
    }
    void SetActiveButton(bool state) {
        shopButton.interactable = state;
        playButton.interactable = state;
        shopButton.interactable = state;
    }

    IEnumerator ChangeLevelCoinText() {
        float timer = 0f;
        float duration = 1f;
        int startValue = 0;
        int endValue = (int)CurrentCurrency.Instance.levelCoin;

        while (timer < duration) {
            float progress = timer / duration;
            int newValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, progress));
            levelBonusText.text = newValue.ToString();
            timer += Time.deltaTime;
            yield return null;
        }

        levelBonusText.text = endValue.ToString();
    }

    IEnumerator ChangeEarnCoinText() {
        float timer = 0;
        float duration = 1f;
        int startValue = 0;
        int endValue = (int)earnCoin;

        while (timer < duration) {
            float progress = timer / duration;
            int newValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, progress));
            earnCoinText.text = newValue.ToString();
            timer += Time.deltaTime;
            yield return null;
        }

        earnCoinText.text = endValue.ToString();
    }
    IEnumerator ChangeMyCoinText() {
        yield return new WaitForSeconds(1);
        CurrentCurrency.Instance.UpdateCurrentcy(Buff.Coin, CurrentCurrency.Instance.levelCoin) ;
        float timer = 0;
        float duration = 1f;
        int startValue = (int)CurrentCurrency.Instance.previousGold;
        int endValue = (int)CurrentCurrency.Instance.CurrentGold;

        while (timer < duration) {
            float progress = timer / duration;
            int newValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, progress));
            myCoinText.text = newValue.ToString();
            timer += Time.deltaTime;
            yield return null;
        }

        myCoinText.text = endValue.ToString();
    }

    private void OnDisable() {
        CurrentCurrency.Instance.PreviousGoldUpdate();
    }
}
