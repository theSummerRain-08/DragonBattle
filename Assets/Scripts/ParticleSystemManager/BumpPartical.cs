using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpPartical : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    private void OnEnable() {
        _particleSystem.Play();
        StartCoroutine(DisplayThisObj());   
    }
    IEnumerator DisplayThisObj() { 
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

}
