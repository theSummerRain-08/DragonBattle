using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpAnimController : MonoBehaviour
{
    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void OnEnable() {
        animator.SetTrigger("Active");
    }
}
