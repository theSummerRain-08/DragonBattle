using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
public class MainSoundManager : MonoBehaviour
{
    public static MainSoundManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] AudioSource mainSound;
    [SerializeField] AudioClip gamePlaySound;
    [SerializeField] AudioClip gameReadySound;

    public void ChangeSound() {
        mainSound.clip = mainSound.clip == gamePlaySound ? gameReadySound : gamePlaySound;
        mainSound.Play();
    }
    public void PlaySound() {
        mainSound.volume = 1;
    }
    public void DisplaySound() {
        mainSound.volume = 0;
    }

}
