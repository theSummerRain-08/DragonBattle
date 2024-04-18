using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckyWheelNotiUI : MonoBehaviour
{
    [SerializeField] private Button maskNoti;
    private void Awake() {
        maskNoti.onClick.AddListener(DisableObj);
    }
    void DisableObj() {
        this.gameObject.SetActive(false);
    }
}
