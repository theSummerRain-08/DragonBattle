using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndableActiveNotiUI : MonoBehaviour
{
    [SerializeField] Button mask;
    [SerializeField] Button okButton;

    private void Awake() {
        mask.onClick.AddListener(DisableObject);
        okButton.onClick.AddListener(DisableObject);
    }
    void DisableObject() {
        SoundEffectManager.Instance.ActiveClickSound();
        this.gameObject.SetActive(false);
    }
}
