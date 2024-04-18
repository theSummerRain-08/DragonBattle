using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButtonManager : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    private void OnEnable() {
        buttons[CircleChooseCharUI.charSelectNum].gameObject.SetActive(true);
    }
    private void Update() {
        for (int i = 0; i < buttons.Length; i++) {
            if (i == CircleChooseCharUI.charSelectNum)
                buttons[i].gameObject.SetActive(true);
            else buttons[i].gameObject.SetActive(false);
        }
    }
}
