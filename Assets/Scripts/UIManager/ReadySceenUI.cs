using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ReadySceenUI : MonoBehaviour {
    public RectTransform targetRectTransform;
    public float duration = 1f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float elapsedTime;

    private float positionScale = 694f;

    private void OnEnable() {
        SoundEffectManager.Instance.OpenGateSound();
        float enemyLevelState = CharacterStats.Instance.EnemyLevel - CharacterStats.Instance.PreviousEnemyLevel;
        float popUpTargetPosition = CharacterStats.Instance.EnemyLevel - 1;
        /*  Biến theo dõi sự thay đổi của level enemy:
            popUpTargetPosition = positionScale * [0, 1, 2, 3,.....]

          + Nếu enemyLevelState > 0: Có nghĩa là level enemy đã tăng, tương đương với người chơi thắng

                 =>   Vị trí popup = popUpTargetPosition - 1 -----> popUpTargetPosition
                      Di chuyển popup xuống dưới 1 đơn vị positionScale

          + Nếu enemyLevelState < 0: Level enemy đã giảm, người chơi thua

                 - Nếu state = -1:
                 => Vị trí popUp: popUpTargetPosition + 1 ------> popUpTargetPosition
                    Di chuyển popup lên trên 1 đơn vị positionScale

                 - Nếu state = -2: 
                 => Vị trí popUp: popUpTargetPosition + 2 ------> popUpTargetPosition
                    Di chuyển popup lên trên 2 đơn vị positonScale                     
       */

        elapsedTime = 0f;
        StartCoroutine(SwitchToVsScreen());
        if (CharacterStats.Instance.PreviousEnemyLevel == 1 && CharacterStats.Instance.EnemyLevel == 1) {
            targetRectTransform.localPosition =
                new Vector3(targetRectTransform.localPosition.x, -positionScale * (popUpTargetPosition - 1), targetRectTransform.position.z);
            startPosition = targetRectTransform.localPosition;
            endPosition = startPosition + new Vector3(0, -positionScale, 0);
            StartCoroutine(MoveToTargetPosition());
        }
        if (enemyLevelState > 0) {
            targetRectTransform.localPosition =
                new Vector3(targetRectTransform.localPosition.x, -positionScale * (popUpTargetPosition - 1), targetRectTransform.position.z);
            startPosition = targetRectTransform.localPosition;
            endPosition = startPosition + new Vector3(0, -positionScale, 0);
            StartCoroutine(MoveToTargetPosition());
        }
        if (enemyLevelState < 0) {
            if (enemyLevelState == -1) {
                targetRectTransform.localPosition =
                new Vector3(targetRectTransform.localPosition.x, -positionScale * (popUpTargetPosition + 1), targetRectTransform.position.z);
                startPosition = targetRectTransform.localPosition;
                endPosition = startPosition + new Vector3(0, positionScale, 0);
                StartCoroutine(MoveToTargetPosition());
            }
            if (enemyLevelState == -2) {
                targetRectTransform.localPosition =
                new Vector3(targetRectTransform.localPosition.x, -positionScale* (popUpTargetPosition + 2), targetRectTransform.position.z);
                startPosition = targetRectTransform.localPosition;
                endPosition = startPosition + new Vector3(0, positionScale * 2, 0);
                StartCoroutine(MoveToTargetPosition());
            }
        }
    }

    IEnumerator SwitchToVsScreen() {
        yield return new WaitForSeconds(GameConstants.TimeToChange);
        FadeInOutManager.Instance.FadeIn();
        yield return new WaitForSeconds(2);
        PanelManager.Instance.SwitchActiveUI(GameUI.ReadyScreen, GameUI.VsScreen);
    }

    private IEnumerator MoveToTargetPosition() {
        yield return new WaitForSeconds(1f);
        while (elapsedTime < duration) {
            targetRectTransform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        targetRectTransform.localPosition = endPosition; 
    }

}
