using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    public static EnemySoundManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [SerializeField] private AudioSource enemyVoice;
    [SerializeField] private AudioClip takeDmgSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip skillSound;
    public void SetTakeDmgSound() {
        enemyVoice.clip = takeDmgSound;
        enemyVoice.Play();
    }
    public void SetDieSound() {
        enemyVoice.clip = dieSound;
        enemyVoice.Play();
    }
    public void SetSkillSound() {
        enemyVoice.clip = skillSound;
        enemyVoice.Play();
    }
}
