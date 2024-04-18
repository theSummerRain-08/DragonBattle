using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldObj : MonoBehaviour
{
    private void OnEnable() {
        StartCoroutine(DestroyThisObj());
    }
    IEnumerator DestroyThisObj() {
        yield return new WaitForSeconds(GameConstants.TimeActiveShield);
        PlayerController.hasShield = false;
        this.gameObject.SetActive(false);
    }
}
