using AirFishLab.ScrollingList.Demo;
using AirFishLab.ScrollingList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static GameManager;
using Unity.VisualScripting;
public class UpdateFocusBox : MonoBehaviour {
    [SerializeField] Image playerImg;
    [SerializeField] Sprite[] playerTransform;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] TextMeshProUGUI priceText;

    [SerializeField] Button activedButton;
    [SerializeField] Button unlockButton;
    [SerializeField] GameObject notice;
    [SerializeField] TextMeshProUGUI noticeText;
    private int currentBoxValue;
    [SerializeField] Image[] transformButton;
    [SerializeField] Image[] transformUI;
    [SerializeField] private CircularScrollingList circularScrollingList;
    private bool donotChangeImg;
    private void OnEnable() {
        donotChangeImg = true;
        SwitchActiveButton();
        playerImg.sprite = playerTransform[0];
        playerImg.SetNativeSize();
        currentBoxValue = 0;
        levelText.text = "0";
        powerText.text = "1000";
        priceText.text = "FREE";
        unlockButton.onClick.AddListener(UnlockButton);
    }

    public void OnFocusingBoxChanged(ListBox prevFocusingBox, ListBox curFocusingBox) {
        currentBoxValue = UpdateNumber(((IntListBox)curFocusingBox).Content);

        //__________________________Render Transform UI _______________________________
        float minRenderValue = currentBoxValue - 2;
        if (minRenderValue < 0) minRenderValue = 21 + minRenderValue;
        float maxRenderValue = currentBoxValue + 2;
        if (maxRenderValue > 20) maxRenderValue = -21 + maxRenderValue;
        for (int i = 0; i < transformButton.Length; i++) {
            if (minRenderValue < maxRenderValue) {
                if (i >= minRenderValue && i <= maxRenderValue)
                    transformButton[i].gameObject.SetActive(true);
                else transformButton[i].gameObject.SetActive(false);
            }
            if (minRenderValue > maxRenderValue) {
                if (i < minRenderValue && i > maxRenderValue)
                    transformButton[i].gameObject.SetActive(false);
                else transformButton[i].gameObject.SetActive(true);
            }
        }


        //_________________________Update player profile ______________________________
        if (!donotChangeImg) return;

        playerImg.sprite = playerTransform[currentBoxValue];
        playerImg.SetNativeSize();
        levelText.text = currentBoxValue.ToString();
        powerText.text = GameConstants.powerLevel[currentBoxValue].ToString();
        priceText.text = GameConstants.goldToUnlockLevel[currentBoxValue].ToString();
        if (priceText.text == "0") priceText.text = "FREE";

        //________________________Unlock - Actived_____________________________________
        switch (CircleChooseCharUI.charSelectNum) {
            case 0:
                CheckActiveButon(Instance.GokuTransformLevel, currentBoxValue);
                break;
            case 1:
                CheckActiveButon(Instance.VegetaTransformLevel, currentBoxValue);
                break;
            case 2:
                CheckActiveButon(Instance.TrunkTransformLevel, currentBoxValue);
                break;
            case 4:
                CheckActiveButon(Instance.GohanTransformLevel, currentBoxValue);
                break;
        }

        //________________________RPGA - Effect_____________________________________
        for (int i = 0; i < transformButton.Length; i++) {
            if (i == currentBoxValue) {
                Color currentColor = transformButton[i].color;
                currentColor.a = 1;
                transformButton[i].color = currentColor;
            } else {
                Color currentColor = transformButton[i].color;
                currentColor.a = 0.4f;
                transformButton[i].color = currentColor;
            }
        }
        for (int i = 0; i < transformUI.Length; i++) {
            if (i == currentBoxValue) {
                Color currentColor = transformUI[i].color;
                currentColor.a = 1;
                transformUI[i].color = currentColor;
            } else {
                Color currentColor = transformUI[i].color;
                currentColor.a = 0.4f;
                transformUI[i].color = currentColor;
            }
        }
    }
    void CheckActiveButon(float characterTransformLevel, float boxValue) {
        if (characterTransformLevel >= boxValue) {
            SwitchActiveButton();
        } else {
            activedButton.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(true);
        }
    }
    public int UpdateNumber(int value) {
        return value - 1;
    }

    void UnlockButton() {
        switch (CircleChooseCharUI.charSelectNum) {
            case 0:
                if (CheckForUnlock(Instance.GokuTransformLevel)) {
                    DoUnlockTransform(CharacterToSelect.Goku);
                }
                if (currentBoxValue - Instance.GokuTransformLevel >= 2) {
                    ActiveLevelNotice(currentBoxValue);
                }
                break;
            case 1:
                if (CheckForUnlock(Instance.VegetaTransformLevel)) {
                    DoUnlockTransform(CharacterToSelect.Vegeta);
                }
                if (currentBoxValue - Instance.VegetaTransformLevel >= 2) {
                    ActiveLevelNotice(currentBoxValue);
                }
                break;
            case 2:
                if (CheckForUnlock(Instance.TrunkTransformLevel)) {
                    DoUnlockTransform(CharacterToSelect.Trunk);
                }
                if (currentBoxValue - Instance.TrunkTransformLevel >= 2) {
                    ActiveLevelNotice(currentBoxValue);
                }
                break;
            case 4:
                if (CheckForUnlock(Instance.GohanTransformLevel)) {
                    DoUnlockTransform(CharacterToSelect.Gohan);
                }
                if (currentBoxValue - Instance.GohanTransformLevel >= 2) {
                    ActiveLevelNotice(currentBoxValue);
                }
                break;
        }
    }
    bool CheckForUnlock(float value) {
        return (currentBoxValue - value == 1); 
    }
    void DoUnlockTransform(CharacterToSelect character) {
        if (CurrentCurrency.Instance.CanUnlockItemWithPrice(GameConstants.goldToUnlockLevel[currentBoxValue])) {
            ActiveSuccessNotice();
            Instance.UnlockNextTransformLevel(character);
            SwitchActiveButton();
        } else ActiveMoneyNotice();
    }
    void ActiveSuccessNotice() { 
        notice.SetActive(true);
        SoundEffectManager.Instance.ActiveCelebrateSound();
        noticeText.text =
            "Cool! You have unlocked new Saiyan's transform";
    }
    void ActiveMoneyNotice() {
        notice.SetActive(true);
        noticeText.text =
            "You do not have enough money";
    }
    void ActiveLevelNotice(float currentLevel) {
        notice.SetActive(true);
        noticeText.text =
            "Level " + (currentLevel) + " is required to upgrade to previous level " + (currentLevel-1);
    }
    void SwitchActiveButton() {
        activedButton.gameObject.SetActive(true);
        unlockButton.gameObject.SetActive(false);
    }
    public void BackToDefault() {
        Color currentColor = transformUI[0].color;
        currentColor.a = 1;
        transformUI[0].color = currentColor;
        transformButton[0].color = currentColor;

        donotChangeImg = false;
        playerImg.sprite = playerTransform[0];
        playerImg.SetNativeSize();
        circularScrollingList.SelectContentID(0);
    }
    private void OnDisable() {
        BackToDefault();
    }
}
