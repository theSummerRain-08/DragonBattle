using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AdsManager : MonoBehaviour
{
    [SerializeField] VideoPlayer ads;
    [SerializeField] Button skipButton;
    [SerializeField] GameObject rewardNoti;
    [SerializeField] private int countdownSeconds;
    [SerializeField] TextMeshProUGUI skipText;
    private void Awake() {
        skipButton.onClick.AddListener(() => { this.gameObject.SetActive(false); });
    }
    private void OnEnable() {
        countdownSeconds = 5;
        MainSoundManager.Instance.DisplaySound();
        ads.Play();
        StartCoroutine(Countdown());
    }
    private void OnDisable() {
        MainSoundManager.Instance.PlaySound();
        ads.Stop();
        rewardNoti.SetActive(true);
        CurrentCurrency.Instance.UpdateCurrentcy(Buff.Coin, 200);
    }
    IEnumerator Countdown() {
        skipButton.interactable = false;

        while (countdownSeconds > 0) {
            skipText.text = "Skip in " + countdownSeconds;
            yield return new WaitForSeconds(1);
            countdownSeconds--;
        }
        skipText.text = "Skip";
        skipButton.interactable = true;
    }
}
