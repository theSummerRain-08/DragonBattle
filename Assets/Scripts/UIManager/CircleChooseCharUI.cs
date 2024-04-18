using AirFishLab.ScrollingList;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CircleChooseCharUI : MonoBehaviour {
    [SerializeField] GameObject[] TransformScroll;
    [SerializeField] UpdateFocusBox[] scroll;
    [SerializeField] RectTransform[] charUI;
    [SerializeField] RectTransform circle;
    bool isTransitioning = false;
    float transitionDuration = 0.7f;
    public static int charSelectNum;
    [SerializeField] TextMeshProUGUI charName;
    [SerializeField] GameObject notiMask;
    [SerializeField] private LayerMask[] layerMask;
    private void OnEnable() {
        charSelectNum = (int)CharacterSelectManager.characterToSelect;
        if (CharacterSelectManager.characterToSelect == CharacterToSelect.Gohan) charSelectNum = 4;
        previousNum = 0;
    }
    void Update() {
        if (Input.GetMouseButton(0)) {
            if (notiMask.activeSelf) return;
            CheckMouseInput();
        }
    }
    void CheckMouseInput() {
        if (RightHit().collider != null && !isTransitioning) {
            StartCoroutine(TransitionAllCharUI(1));
        }
        if (LeftHit().collider != null && !isTransitioning) {
            StartCoroutine(TransitionAllCharUI(-1));
        }
    }

    private RaycastHit2D RightHit() {
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask[1]);
    }

    private RaycastHit2D LeftHit() {
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask[0]);
    }
    int previousNum = 0;
    IEnumerator TransitionAllCharUI(int value) {    
        if (value == 1) {
            charSelectNum++;
            if (charSelectNum >= charUI.Length) charSelectNum = 0;
            previousNum = charSelectNum - 1;
            if (previousNum < 0) previousNum = charUI.Length - 1;
        }
        if (value == -1) {
            charSelectNum--;
            if (charSelectNum < 0) charSelectNum = charUI.Length - 1;
            previousNum = charSelectNum + 1;
            if (previousNum >= charUI.Length) previousNum = 0;
        }

        isTransitioning = true;

        Vector3[] startPositions = new Vector3[charUI.Length];
        Vector3[] targetPositions = new Vector3[charUI.Length];
        Vector3[] startScales = new Vector3[charUI.Length];
        Vector3[] targetScales = new Vector3[charUI.Length];

        for (int i = 0; i < charUI.Length; i++) {
            if (value == 1) {
                startPositions[i] = charUI[i].position;
                startScales[i] = charUI[i].localScale;
                targetPositions[i] = charUI[(i + 1) % charUI.Length].position;
                targetScales[i] = charUI[(i + 1) % charUI.Length].localScale;
            } else if (value == -1) {
                startPositions[i] = charUI[i].position;
                startScales[i] = charUI[i].localScale;
                targetPositions[i] = charUI[(i + charUI.Length - 1) % charUI.Length].position;
                targetScales[i] = charUI[(i + charUI.Length - 1) % charUI.Length].localScale;
            }
        }

        Vector3 startRotation = circle.eulerAngles;
        Vector3 targetRotation = startRotation - new Vector3(0f, 0f, value * 360f / charUI.Length);

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration) {
            for (int i = 0; i < charUI.Length; i++) {
                charUI[i].position = Vector3.Lerp(startPositions[i], targetPositions[i], elapsedTime / transitionDuration);
                charUI[i].localScale = Vector3.Lerp(startScales[i], targetScales[i], elapsedTime / transitionDuration);
            }

            circle.eulerAngles = Vector3.Lerp(startRotation, targetRotation, elapsedTime / transitionDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < charUI.Length; i++) {
            charUI[i].position = targetPositions[i];
            charUI[i].localScale = targetScales[i];
        }
        charName.text = GameConstants.playerNameForTransform[charSelectNum];
        StartCoroutine(ChangeActive(charSelectNum, previousNum));

        circle.eulerAngles = targetRotation;
        isTransitioning = false;
    }

    Vector3 originalPosition;
    void UpdateOrinalPosition(Vector3 position) {
        originalPosition = position;
    }
    IEnumerator ChangeActive(int num, int previousNum) {
        TransformScroll[num].SetActive(true);

        UpdateOrinalPosition(TransformScroll[previousNum].transform.position);
        Vector3 targetPosition = originalPosition + new Vector3(-1000f, 0f, 0f); 
        TransformScroll[previousNum].transform.position = targetPosition;
        if (scroll[previousNum] != null) {
            scroll[previousNum].BackToDefault();
        }
        yield return new WaitForSeconds(0.25f);

        TransformScroll[previousNum].SetActive(false);
        TransformScroll[previousNum].transform.position = originalPosition;
    }

    private void OnDisable() {
        TransformScroll[previousNum].SetActive(false);
        TransformScroll[previousNum].transform.position = originalPosition;
    }
}
