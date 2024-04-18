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
    private void Start() {
        InvokeRepeating("ChangePosition", 0f, 7f);
    }

    private void ChangePosition() {
        Vector3 newPosition = new Vector3(transform.position.x, Random.Range(-3f, 1f), transform.position.z);

        
        transform.position = newPosition;
        float randomNum = Random.Range(0, 5); 
        RandomBuff(randomNum);
    }
    void RandomBuff(float num) {
        switch (num) {
            case 1:
                int randomBean = Random.Range(0, 3);
                beanBuff[randomBean].transform.position = this.transform.position;
                beanBuff[randomBean].SetActive(true); 
                break;
            case 2:
                shieldBuff.transform.position =  transform.position;
                shieldBuff.SetActive(true);
                break;
            case 3:
                for (int i = 0; i < coinBuff.Length; i++) { 
                    coinBuff[i].SetActive(true);
                    coinBuff[i].transform.position = new Vector3(transform.position.x + i * 1, transform.position.y, transform.position.z);
                }
                break;
        }
    }
}
