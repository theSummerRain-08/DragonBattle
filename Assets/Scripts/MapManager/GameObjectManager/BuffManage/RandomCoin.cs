using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomCoin : MonoBehaviour
{

    private void Update() {
        Move();
        if (GameUltis.ExitLeftScreen(this.transform.position)) {
            gameObject.SetActive(false);
        }
    }

    public void Move() {
        float newPositionX = transform.position.x - SpawnPointManager.spawnObjSpeed * Time.deltaTime;
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            gameObject.SetActive(false);
            CurrentCurrency.Instance.UpdateCurrentcy(Buff.Coin, 1);
        }

    }
}
