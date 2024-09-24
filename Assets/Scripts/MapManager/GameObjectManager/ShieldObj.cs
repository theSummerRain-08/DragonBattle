using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldObj : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    private void OnEnable() {
        StartCoroutine(DestroyThisObj());
        _particleSystem.Play();
    }
    IEnumerator DestroyThisObj() {
        yield return new WaitForSeconds(GameConstants.TimeActiveShield);
        PlayerController.hasShield = false;
        this.gameObject.SetActive(false);
    }
}
