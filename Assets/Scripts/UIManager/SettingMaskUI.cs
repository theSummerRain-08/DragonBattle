using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMaskUI : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Button musicBgButton;
    [SerializeField] private Button soundEffButton;

    [SerializeField] private Button maskUI;

    public static bool hasMusic = true;
    public static bool hasSoundEff = true;

    [SerializeField] private Sprite[] musicSprite;
    [SerializeField] private Sprite[] soundSprite;
    private void Awake() {
        musicBgButton.onClick.AddListener(ClickMusicButton);
        soundEffButton.onClick.AddListener(ClickSoundButton);
        maskUI.onClick.AddListener(DiableMask);
    }
    void Update()
    {
        CheckForImage();    
    }

    void ClickMusicButton() {
        SoundEffectManager.Instance.ActiveClickSound();
        hasMusic = hasMusic ? false : true;
        if (hasMusic) MainSoundManager.Instance.PlaySound();
        else MainSoundManager.Instance.DisplaySound();
    }
    void ClickSoundButton() {
        SoundEffectManager.Instance.ActiveClickSound();
        hasSoundEff = hasSoundEff ? false : true;

        if (hasSoundEff) SoundEffectManager.Instance.PlaySoundEff();
        else SoundEffectManager.Instance.StopSoundEff();
    }

    void CheckForImage() {
        musicBgButton.image.sprite = hasMusic ? musicSprite[0] : musicSprite[1];
        soundEffButton.image.sprite = hasSoundEff ? soundSprite[0] : soundSprite[1];
    }
    void DiableMask() { 
        this.gameObject.SetActive(false);
    }
}
