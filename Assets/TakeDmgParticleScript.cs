using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDmgParticleScript : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    private void OnEnable() {
        _particleSystem.Play();
        StartCoroutine(WaitForParticleToEnd());

    }
    IEnumerator WaitForParticleToEnd() {
        while (_particleSystem.isPlaying) {
            yield return null;
        }

        ObjectPooling.Instance.DeSpawn(this.gameObject);
    }
}
