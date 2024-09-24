using AirFishLab.ScrollingList;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CircleChooseCharUI : MonoBehaviour {
    [SerializeField] private int[] sortedNumbers;
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
    [SerializeField] ParticleSystem auraparticle;
    [SerializeField] ParticleSystem circleParticle;
    
    private void OnEnable() {
        charSelectNum = (int)CharacterSelectManager.characterToSelect;
        if (CharacterSelectManager.characterToSelect == CharacterToSelect.Gohan) charSelectNum = 4;
        if (CharacterSelectManager.characterToSelect == CharacterToSelect.None) charSelectNum = 3;
        previousNum = 0;
        StartCoroutine(SetActiveCircle());
    }
    IEnumerator SetActiveCircle() {
        yield return new WaitForSeconds(0.8f);
        circleParticle.gameObject.SetActive(true);
        auraparticle.gameObject.SetActive(true);
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
        circleParticle.gameObject.SetActive(false);
        auraparticle.Stop();

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
        SortUIPriority(charSelectNum);

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

        circleParticle.gameObject.SetActive(true);
        auraparticle.Play();
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
    void SortUIPriority(int charSelectNum) {

        int[] indices = { 0, 4, 3, 2, 1};
        int selectedNumber = charSelectNum;

        // Xác định chỉ số của số được chọn
        int selectedIndex = Array.IndexOf(indices, selectedNumber);

        // Xác định 2 số liền kề của số được chọn
        int adjacent1 = (selectedIndex + 1) % indices.Length; // Số liền kề 1
        int adjacent2 = (selectedIndex + 4) % indices.Length; // Số liền kề 2

        // Tạo một mảng mới để lưu trữ dãy số đã sắp xếp
        sortedNumbers = new int[indices.Length];

        // Đặt số được chọn vào vị trí đầu tiên của mảng đã sắp xếp
        sortedNumbers[4] = selectedNumber;
        // Đặt 2 số liền kề vào vị trí thứ 2 và thứ 3 của mảng đã sắp xếp
        sortedNumbers[3] = indices[adjacent1];
        sortedNumbers[2] = indices[adjacent2];

        int index = 1;
        for (int i = 0; i < indices.Length; i++) {
            if (i != selectedIndex && i != adjacent1 && i != adjacent2) {
                sortedNumbers[index--] = indices[i];
            }
        }

        SortCharUIByNumbers();
       

    }
    private void SortCharUIByNumbers() {
        // Tạo một mảng mới lưu vị trí mới của các phần tử trong charUI[]
        int[] newIndex = new int[charUI.Length];

        // Tạo một bản sao của mảng sortedNumbers để không làm thay đổi mảng gốc
        int[] sortedCopy = (int[])sortedNumbers.Clone();

        // Sắp xếp mảng sortedCopy để tìm vị trí mới của các phần tử
        Array.Sort(sortedCopy);

        // Duyệt qua mảng sortedNumbers để tìm vị trí mới của từng phần tử trong charUI[]
        for (int i = 0; i < sortedNumbers.Length; i++) {
            // Tìm vị trí của phần tử trong mảng đã sắp xếp
            int index = Array.IndexOf(sortedCopy, sortedNumbers[i]);

            // Lưu vị trí mới của phần tử
            newIndex[index] = i;
        }

        charUI[0].SetSiblingIndex(newIndex[0]);
        charUI[4].SetSiblingIndex(newIndex[1]);
        charUI[1].SetSiblingIndex(newIndex[4]);
        charUI[2].SetSiblingIndex(newIndex[3]);
        charUI[3].SetSiblingIndex(newIndex[2]);
    }

    private void OnDisable() {
        circleParticle.gameObject.SetActive(false);
        auraparticle.gameObject.SetActive(false);
        if (previousNum != charSelectNum) {
            TransformScroll[previousNum].SetActive(false);
            TransformScroll[previousNum].transform.position = originalPosition;
        }
    }
}
