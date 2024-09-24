using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private GameObject[] beanBuff;
    [SerializeField] private GameObject[] coinBuff;
    [SerializeField] private GameObject shieldBuff;

    public static float spawnObjSpeed = 6f;
    private void OnEnable() {
        InvokeRepeating("ChangePosition", 0f, 9f);
    }


    private void ChangePosition() {
        Vector3 newPosition = new Vector3(transform.position.x, Random.Range(-3f, 1f), transform.position.z); 
        transform.position = newPosition;
        float randomNum = Random.Range(0, 11); 
        RandomBuff(randomNum);
    }
    void RandomBuff(float num) {
        if (num >= 0 && num <= 3) {
            int randomBean = Random.Range(0, 3);
            if (beanBuff[randomBean].activeSelf) return;
            beanBuff[randomBean].transform.position = this.transform.position;
            beanBuff[randomBean].SetActive(true);
        } else if (num > 3 && num <= 5) {
            if (shieldBuff.activeSelf) return;
            shieldBuff.transform.position = transform.position;
            shieldBuff.SetActive(true);
        } else if (num > 5 && num <= 8) {
            for (int i = 0; i < coinBuff.Length; i++) {
                if (!coinBuff[i].activeSelf) {
                    coinBuff[i].SetActive(true);
                    coinBuff[i].transform.position = new Vector3(transform.position.x + i * 1, transform.position.y, transform.position.z);
                }
            }
        }
    }
    private void OnDisable() {
        CancelInvoke("ChangePosition");
        foreach (GameObject bean in beanBuff) {
            bean.SetActive(false);
        }
        foreach (GameObject coin in coinBuff) {
            coin.SetActive(false);
        }
        shieldBuff.SetActive(false);
    }
}
