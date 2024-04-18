using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.UIElements;
using static GameUltis;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance = null;
    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy() {
        Instance = null;
    }

    [SerializeField] Transform holder;
    [SerializeField] protected List<GameObject> poolObj;
    [SerializeField] Transform player;

    public void SpawnObject(GameObject objectToSpawn, Vector3 position) {
        Vector3 spawnPoint = position;
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 0f);
        GetObjectFromBool(objectToSpawn, spawnPoint, spawnRotation);
    }

    protected void GetObjectFromBool(GameObject _poolObj, Vector3 spawnPoin, Quaternion spawnRotation) {
        if (poolObj.Count > 0) {
            foreach (GameObject poolObj in poolObj) {
                if (poolObj.name == _poolObj.name) {
                    this.poolObj.Remove(poolObj);
                    poolObj.transform.position = spawnPoin;
                    poolObj.SetActive(true);
                    poolObj.transform.parent = holder;

                    return;
                }
            }
        }
        GameObject newPoolObj = Instantiate(_poolObj);
        newPoolObj.name = _poolObj.name;
        newPoolObj.transform.position = spawnPoin;
        newPoolObj.transform.parent = holder;


    }

    public void SpawnObject(GameObject objectToSpawn, Vector3 position, bool parentOfPlayer) {
        Vector3 spawnPoint = position;
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 0f);
        GetObjectFromBool(objectToSpawn, spawnPoint, spawnRotation, parentOfPlayer);
    }

    protected void GetObjectFromBool(GameObject _poolObj, Vector3 spawnPoin, Quaternion spawnRotation, bool parentOfPlayer) {
        if (poolObj.Count > 0) {
            foreach (GameObject poolObj in poolObj) {
                if (poolObj.name == _poolObj.name) {
                    this.poolObj.Remove(poolObj);
                    poolObj.transform.position = spawnPoin;
                    poolObj.SetActive(true);
                    if (parentOfPlayer) {
                        poolObj.transform.parent = player;
                    }
                    return;
                }
            }
        }
        GameObject newPoolObj = Instantiate(_poolObj);
        newPoolObj.name = _poolObj.name;
        newPoolObj.transform.position = spawnPoin;
        if (parentOfPlayer) {
            newPoolObj.transform.parent = player;
        }


    }
    public void DeSpawn(GameObject _poolObj) {
        poolObj.Add(_poolObj);
        _poolObj.SetActive(false);
    }


    
}
