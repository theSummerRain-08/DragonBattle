using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    public static CharacterSound Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    [SerializeField] private AudioSource playerVoice;
    [SerializeField] private AudioClip[] atkSounds;
    [SerializeField] private AudioClip transformSound;
    [SerializeField] private AudioClip takeDmgSound;
    [SerializeField] private AudioClip dieSound;

    public void SetAtkSound(AttackType type) { 
        playerVoice.clip = atkSounds[(int)type];
        playerVoice.Play(); 
    }
    public void SetTransformSound() { 
        playerVoice.clip = transformSound;
        playerVoice.Play();
    }
    public void SetTakeDmgSound() { 
        playerVoice.clip = takeDmgSound;
        playerVoice.Play();
    }
    public void SetDieSound() {
        playerVoice.clip = dieSound;
        playerVoice.Play();
    }
}
