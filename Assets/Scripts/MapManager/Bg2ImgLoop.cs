using UnityEngine;

public class Bg2ImgLoop : MonoBehaviour {
    [SerializeField] private RectTransform img2;
    private float speed = 1200f;
    private Vector3 startPostion;

    private void Awake() {
        startPostion = img2.localPosition;
    }

    private void Update() {
       img2.localPosition -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        if (img2.localPosition.x < -img2.rect.width) {
            img2.localPosition = startPostion;
        }
    }
}
