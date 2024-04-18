using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour {
    [SerializeField] ParticleSystem[] handParticle;
    [SerializeField] GameObject[] handObjects;


    [SerializeField] ParticleSystem kamehaParticle;
    [SerializeField] GameObject kamehaObj;

    [SerializeField] ParticleSystem kameha2Particle;
    [SerializeField] GameObject kameha2Obj;
    public void PlayHandParticle() {
        for (int i = 0; i < handParticle.Length; i++) {
            handParticle[i].Play();
            handObjects[i].SetActive(true);
        }
    }

    public void StopHandParticle() {
        for (int i = 0; i < handParticle.Length; i++) {
            handParticle[i].Stop();
            handObjects[i].SetActive(false);
        }
    }

    public void PlayKamehaEff() {
        kamehaObj.SetActive(true);
        kamehaParticle.Play();
        StartCoroutine(DisplayKameha());
    }

    IEnumerator DisplayKameha() {
        yield return new WaitForSeconds(3.2f);
        DisplayKamehaEff();
    }
    public void DisplayKamehaEff() {
        kamehaObj.SetActive(false);
        kamehaParticle.Stop();
    }

    public void PlayKameha2Eff() {
        kameha2Obj.SetActive(true);
        kameha2Particle.Play();
        StartCoroutine(DisplayKameha2());
    }

    IEnumerator DisplayKameha2() {
        yield return new WaitForSeconds(3.2f);
        DisplayKameha2Eff();
    }
    public void DisplayKameha2Eff() {
        kameha2Obj.SetActive(false);
        kameha2Particle.Stop();
    }
}
