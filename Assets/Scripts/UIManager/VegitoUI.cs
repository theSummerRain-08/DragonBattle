using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VegitoUI : MonoBehaviour
{
    [SerializeField] GameObject[] textMeshProUGUI;
    [SerializeField] GameObject commingSoon;
    private void OnEnable() {
        for (int i = 0; i < textMeshProUGUI.Length; i++) {
            textMeshProUGUI[i].SetActive(false);
        }
        commingSoon.SetActive(true);
    }
    private void OnDisable() {
        for (int i = 0; i < textMeshProUGUI.Length; i++) {
            textMeshProUGUI[i].SetActive(true);
        }
        commingSoon.SetActive(false);
    }
}
