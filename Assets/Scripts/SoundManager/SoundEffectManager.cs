using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {
    public static SoundEffectManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] AudioSource effectSound;

    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip[] collectItemSound;
    [SerializeField] AudioClip earnCoinSound;
    [SerializeField] AudioClip []skillSound;
    [SerializeField] AudioClip celebrateSound;
    [SerializeField] AudioClip openGateSound;
    public void ActiveClickSound() {
        effectSound.clip = clickSound;
        effectSound.Play();
    }

    public void ActiveCollectItemSound(Item itemType) { 
        effectSound.clip = collectItemSound[(int)itemType];
        effectSound.Play();
    }
    public void EarnCoinSound() { 
        effectSound.clip = earnCoinSound;
        effectSound.Play();
    }
    public void ActiveSkillSound(int skilNumber) { 
        effectSound.clip = skillSound[skilNumber];
        effectSound.Play();
    }
    public void ActiveCelebrateSound() { 
        effectSound.clip = celebrateSound;
        effectSound.Play();
    }
    public void OpenGateSound() {
        effectSound.clip = openGateSound;
        effectSound.Play();
    }
    public void PlaySoundEff() {
        effectSound.volume = 1;
    }
    public void StopSoundEff() {
        effectSound.volume = 0;
    }

}
