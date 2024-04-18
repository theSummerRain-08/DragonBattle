using AirFishLab.ScrollingList.Demo;
using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
public class RenderForcusBox : MonoBehaviour {
    private int currentBoxValue;
    [SerializeField] Image[] transformButton;
    private void OnEnable() {
        currentBoxValue = 0;

    }
    public void OnFocusingBoxChanged(ListBox prevFocusingBox, ListBox curFocusingBox) {
        currentBoxValue = UpdateNumber(((IntListBox)curFocusingBox).Content);
        //__________________________Render Transform UI _______________________________
        float minRenderValue = currentBoxValue - 2;
        if (minRenderValue < 0) minRenderValue = 21 + minRenderValue;
        float maxRenderValue = currentBoxValue + 2;
        if (maxRenderValue > 20) maxRenderValue = -21 + maxRenderValue;
        for (int i = 0; i < transformButton.Length; i++) {
            if (minRenderValue < maxRenderValue) {
                if (i >= minRenderValue && i <= maxRenderValue)
                    transformButton[i].gameObject.SetActive(true);
                else transformButton[i].gameObject.SetActive(false);
            }
            if (minRenderValue > maxRenderValue) {
                if (i < minRenderValue && i > maxRenderValue)
                    transformButton[i].gameObject.SetActive(false);
                else transformButton[i].gameObject.SetActive(true);
            }
        }

    }
    public int UpdateNumber(int value) {
        return value - 1;
    }
}
