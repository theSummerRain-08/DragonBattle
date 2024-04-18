using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheel : MonoBehaviour {
    [Header("Wheel number")]
    public int numberGift = 8;
    public float timeRotate;
    public float numberRotate;

    private const float CIRCLE = 360.0f;
    private float angelOfOneGift;

    private float currentTime;
    public AnimationCurve curve;
    [Header("Button - UI")]
    [SerializeField] private Button SpinButton;

    [SerializeField] private GameObject priceNoti;
    [SerializeField] private Image priceNotiImage;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI congrateText;
    [SerializeField] Sprite[] imgSprite;
    int priceValue = 1;
    private void Awake() {
        SpinButton.onClick.AddListener(DoRotateWheel);
    }
    private void Start() {
        angelOfOneGift = CIRCLE / numberGift;

    }
    void DoRotateWheel() {
        SoundEffectManager.Instance.ActiveClickSound();
        StartCoroutine(RotateWheel());
    }
    IEnumerator RotateWheel() {
        float startAngel = transform.eulerAngles.z;
        currentTime = 0;

        int indexGiftRandom = Random.Range(1, numberGift + 1);
        priceValue += indexGiftRandom - 1;
        if (priceValue > numberGift) {
            priceValue = priceValue -8;
        }

        float angelToRotate = (numberRotate * CIRCLE) + angelOfOneGift * indexGiftRandom;
        while (currentTime < timeRotate) {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;

            float angelCurrent = angelToRotate * curve.Evaluate(currentTime / timeRotate);
            this.transform.eulerAngles = new Vector3(0, 0, angelCurrent + startAngel);
        }
        TakeResult(priceValue);
    }
    

    void TakeResult(int result) {
        switch (result) {
            case 1:
                GameManager.Instance.UnlockNextTransformLevel(CharacterToSelect.Goku);
                SetUiNoti(true, 0, "1 Lv");
                break;
            case 2:
                CurrentCurrency.Instance.UpdateCurrentcy(Buff.Coin, 200);
                SetUiNoti(true, 1, "200");
                break;
            case 3:
                CurrentCurrency.Instance.UpdateCurrentcy(Buff.Bean, 1);
                SetUiNoti(true, 2, "1");
                break;
            case 4:
                CurrentCurrency.Instance.UpdateCurrentcy(Buff.Coin, 500);
                SetUiNoti(true, 1, "500");
                break;
            case 5:
                GameManager.Instance.UnlockNextTransformLevel(CharacterToSelect.Goku);
                SetUiNoti(true, 0, "2 Lv");
                break;
            case 6:
                SetUiNoti(false, 3, "Lose");
                break;
            case 7:
                CurrentCurrency.Instance.UpdateCurrentcy(Buff.Coin, 1000);
                SetUiNoti(true, 1, "1000");
                break;
            case 8:
                SetUiNoti(true, 4, "1 Week");
                break;
        }
    }
    void SetUiNoti(bool congrate, int sprite, string text) {
        if (congrate) { SoundEffectManager.Instance.ActiveCelebrateSound(); }
        priceNoti.SetActive(true);
        congrateText.gameObject.SetActive(congrate);
        priceNotiImage.sprite = imgSprite[sprite];
        priceText.text = text;
    }
}
