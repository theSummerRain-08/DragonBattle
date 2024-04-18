using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg1ImgLoop : MonoBehaviour
{
    [SerializeField] private RectTransform img1;
    [SerializeField] private RectTransform img2;
    private float speed = 1200f;
    private Vector3 repeatPostion;
    //private void Awake() {
    //    repeatPostion = img2.localPosition;
    //}

    private void Update() {
        repeatPostion = img2.localPosition;
        img1.localPosition -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        if (img1.localPosition.x < -img1.rect.width) {
            img1.localPosition = new Vector3(repeatPostion.x + img2.rect.width, repeatPostion.y, repeatPostion.z);
        }
    }
}
