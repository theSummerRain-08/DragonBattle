using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetActiveBg : MonoBehaviour {
    [SerializeField] GameObject[] bg;

    void OnEnable()  {
        SetRandomActiveBg();
    }

    void SetRandomActiveBg() {
        if (bg.Length > 0) {
            foreach (GameObject background in bg) {
                background.SetActive(false);
            }
            int randomIndex = Random.Range(0, bg.Length);
            bg[randomIndex].SetActive(true);
        } else {
            Debug.LogError("Không có background nào được gán trong danh sách!");
        }
    }
}
