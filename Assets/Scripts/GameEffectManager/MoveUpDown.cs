using UnityEngine;

public class MoveUpDown : MonoBehaviour {
    [SerializeField] private RectTransform playerRect;
    private bool isMovingUp = true;
    [SerializeField] private float upPositionY = 185f;
    [SerializeField] private float downPositionY = 169f;
    private float moveSpeed = 10f;
    [SerializeField] private float charNum;
    private float stopTime = 1.5f;
    private float timer = 0f;

    private void Update() {
        if (CircleChooseCharUI.charSelectNum == charNum) {
            timer += Time.deltaTime;

            if (isMovingUp) {
                MoveToPosition(upPositionY);
                if (Mathf.Approximately(playerRect.anchoredPosition.y, upPositionY)) {
                    if (timer >= stopTime) {
                        isMovingUp = false;
                        timer = 0f;
                    }
                }
            } else {
                MoveToPosition(downPositionY);
                if (Mathf.Approximately(playerRect.anchoredPosition.y, downPositionY)) {
                    if (timer >= stopTime) {
                        isMovingUp = true;
                        timer = 0f;
                    }
                }
            }
        } else {
            float posY = 177.9f;
            MoveToPosition(posY);
        }
    }

    private void MoveToPosition(float targetY) {
        float step = moveSpeed * Time.deltaTime;
        Vector3 targetPosition = playerRect.anchoredPosition;
        targetPosition.y = Mathf.MoveTowards(targetPosition.y, targetY, step);
        playerRect.anchoredPosition = targetPosition;
    }
}
