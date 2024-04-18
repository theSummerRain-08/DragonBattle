using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 
public class CharacterCurrentSSJUI : MonoBehaviour
{
    [SerializeField] Image playerImg;
    [SerializeField] Sprite[] GokuSprite;
    [SerializeField] Sprite[] VegetaSprite;
    [SerializeField] Sprite[] TrunkSprite;
    [SerializeField] Sprite[] GohanSprite;
    [SerializeField] TextMeshProUGUI playerName;

    private void Update() {
        if (CharacterSelectManager.characterToSelect == CharacterToSelect.Goku) {
            playerImg.sprite = GokuSprite[CharacterStats.Instance.PlayerLevel - 1];
            playerImg.SetNativeSize();
        }
        if (CharacterSelectManager.characterToSelect == CharacterToSelect.Vegeta) {
            playerImg.sprite = VegetaSprite[CharacterStats.Instance.PlayerLevel - 1];
            playerImg.SetNativeSize();
        }
        if (CharacterSelectManager.characterToSelect == CharacterToSelect.Trunk) {
            playerImg.sprite = TrunkSprite[CharacterStats.Instance.PlayerLevel - 1];
            playerImg.SetNativeSize();
        }
        if (CharacterSelectManager.characterToSelect == CharacterToSelect.Gohan) {
            playerImg.sprite = GohanSprite[CharacterStats.Instance.PlayerLevel - 1];
            playerImg.SetNativeSize();
        }
        if (playerName != null) {
            playerName.text = "SSJ." + (CharacterStats.Instance.PlayerLevel - 1);
            playerImg.SetNativeSize();
        }
    }
}
