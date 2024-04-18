using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChooseImg : MonoBehaviour
{
    [SerializeField] Image playerImg;
    [SerializeField] Sprite[] playerSprite;
    [SerializeField] TextMeshProUGUI playerName;

    private void Update() {
        playerImg.sprite = playerSprite[(int)CharacterSelectManager.characterToSelect];
        if (playerName != null) {
            playerName.text = GameConstants.playerName[(int)CharacterSelectManager.characterToSelect];
        }   
    }
}
