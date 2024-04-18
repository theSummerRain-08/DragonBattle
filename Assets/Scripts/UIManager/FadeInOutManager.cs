using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutManager : MonoBehaviour
{
    public static FadeInOutManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
    }
    [SerializeField] GameObject fadeImage;
    [SerializeField] GameObject logInImg;
    private Animator animator;

    public void LogIn() { 
        logInImg.SetActive(false);
    }
    public void FadeIn() {
        animator.SetTrigger("FadeIn");
    }
    public void FadeOut() {
        animator.SetTrigger("FadeOut");
    }
}
