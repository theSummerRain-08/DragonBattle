using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBeenBuff : MonoBehaviour
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
            CharacterStats.Instance.BuffMana();
        }

    }
}
