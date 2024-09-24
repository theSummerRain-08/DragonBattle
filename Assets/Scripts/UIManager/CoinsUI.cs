using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class CoinsUI : MonoBehaviour {
    [SerializeField] Button shopButton;
    [SerializeField] Button rightArrow;
    [SerializeField] Button leftArrow;
    [SerializeField] RectTransform scroll;
    [SerializeField] private float targetPosXRight;
    [SerializeField] private float targetPosXLeft;
    private float duration = 0.3f;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] Button[] payButton;
    [SerializeField] GameObject notiMask;
    [SerializeField] Button adsButton;
    [SerializeField] GameObject videoAdsObj;
    private void Awake() {
        shopButton.onClick.AddListener(ShopButton);
        rightArrow.onClick.AddListener(() => MoveScroll(true));
        leftArrow.onClick.AddListener(() => MoveScroll(false));
        for (int i = 0; i < payButton.Length; i++) {
            payButton[i].onClick.AddListener(PayFuncion);
        }
        adsButton.onClick.AddListener(AdsFuncion);
    }
    private void OnEnable() {
        scroll.anchoredPosition = new Vector2(targetPosXLeft, scroll.anchoredPosition.y);
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
    void MoveScroll(bool moveRight) {
        if (moveRight) {
            StartCoroutine(MoveToPosition(targetPosXRight,duration));
        }
        else StartCoroutine(MoveToPosition(targetPosXLeft,duration));
    }
    void PayFuncion() {
        SoundEffectManager.Instance.ActiveClickSound();
        notiMask.gameObject.SetActive(true);
    }
    void AdsFuncion() { 
        videoAdsObj.gameObject.SetActive(true);
    }
    IEnumerator MoveToPosition(float targetX, float duration) {
        float time = 0;
        Vector2 startPosition = scroll.anchoredPosition;
        float startX = startPosition.x;

        while (time < duration) {
            float newX = Mathf.Lerp(startX, targetX, time / duration);
            scroll.anchoredPosition = new Vector2(newX, startPosition.y);
            time += Time.deltaTime;

            yield return null;
        }

        scroll.anchoredPosition = new Vector2(targetX, startPosition.y);
    }
}
