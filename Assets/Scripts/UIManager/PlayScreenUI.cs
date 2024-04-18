using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CharacterStats;

public class PlayScreenUI : MonoBehaviour {
    [SerializeField] Button[] attackbutton;
    [SerializeField] PlayerController playerController;
    [SerializeField] Button beanButton;
    [Header("Bars")]
    [SerializeField] Image playerHealth;
    [SerializeField] Image playerMana;
    [SerializeField] Image enemyHealth;
    [SerializeField] Image enemyMana;
    [Header("Mask")]
    [SerializeField] private GameObject victoryMask;
    [SerializeField] private GameObject gameOverMask;
    [SerializeField] private GameObject changerEnemyState;
    [SerializeField] TextMeshProUGUI resultText;
    [Header("Game Object")]
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject enemyObj;

    [SerializeField] TextMeshProUGUI enemyLevel;
    private void Awake() {
        for (int i = 0; i < attackbutton.Length; i++) {
            int index = i;
            if (index == 5)
                attackbutton[i].onClick.AddListener(CallTransformFuncion);
            
            else 
                attackbutton[i].onClick.AddListener(() => CallAttackFuncion((AttackType)index));
        }
        beanButton.onClick.AddListener(BeanButton);
    }
    void CallAttackFuncion(AttackType type) {
        if (GameEnum.characterState == CharacterState.Attack || GameEnum.characterState == CharacterState.Attack3) return;
        playerController.Attack(type);
        
    }
    void CallTransformFuncion() {
        if (GameEnum.characterState == CharacterState.Transform) return;
        playerController.UltimateSkill();
        
    }
    private void OnEnable() {
        CurrentCurrency.Instance.PreviousGoldUpdate();
       
    }

    void Update() {
        enemyLevel.text = CharacterSelectManager.enemyState.ToString();
        if (Instance.EnemyHp <= 0) {
            CheckForEnemyState();
        }
        if (Instance.PlayerHp <= 0) {
            CheckPlayerHp();
        }
        UpdateStatusBar(playerHealth, Instance.PlayerHp, Instance.maxPlayerHp);
        UpdateStatusBar(playerMana, Instance.PlayerMana, Instance.maxPlayerMana);
        UpdateStatusBar(enemyHealth, Instance.EnemyHp, Instance.maxEnemyHp);
        UpdateStatusBar(enemyMana, Instance.EnemyMana, Instance.maxEnemyMana);

        CheckInterctiveButton();
        CheckInteractveUltButton();
    }

    private void UpdateStatusBar(Image statusBar, float currentValue, float maxValue) {
        float fillAmountRatio = currentValue / maxValue;
        statusBar.fillAmount = fillAmountRatio;
    }

    public void CannotUpgrade() {
        attackbutton[5].interactable = false;
    }
    public void BeanButton() {
        if (CurrentCurrency.Instance.CurentBean == 0) return;
        SoundEffectManager.Instance.ActiveClickSound();
        Instance.PlusMana(10);
        Instance.BuffHp();
        CurrentCurrency.Instance.UpdateCurrentcy(Buff.Bean, -1);
    }
    void CheckInterctiveButton() {


        if (Instance.PlayerLevel < 4) {
            attackbutton[3].interactable = false;
            attackbutton[4].interactable = false;
        }
        if (Instance.PlayerLevel >= 4) {
            attackbutton[4].interactable = true;
        }
        if (Instance.PlayerLevel >= 7) {
            attackbutton[3].interactable = true;
        }
    }
    void CheckInteractveUltButton() {
        switch (CharacterSelectManager.characterToSelect) {
            case CharacterToSelect.Goku:
                if (Instance.PlayerLevel == GameManager.Instance.GokuTransformLevel + 1)
                    attackbutton[5].interactable = false;
                break;
            case CharacterToSelect.Vegeta:
                if (Instance.PlayerLevel == GameManager.Instance.VegetaTransformLevel + 1)
                    attackbutton[5].interactable = false;
                break;
            case CharacterToSelect.Trunk:
                if (Instance.PlayerLevel == GameManager.Instance.TrunkTransformLevel + 1)
                    attackbutton[5].interactable = false;
                break;
            case CharacterToSelect.Gohan:
                if (Instance.PlayerLevel == GameManager.Instance.GohanTransformLevel + 1)
                    attackbutton[5].interactable = false;
                break;
        }
    }
    void CheckPlayerHp() {
        SetActiveResult(Results.Lose);

    }
    bool ChangeToNextState() {
        float value = Instance.EnemyLevel;
        return (value == 5 || value == 6 || value == 9);
    }
    void CheckForEnemyState() {
        if (ChangeToNextState()) {
            switch (CharacterSelectManager.enemyState) {
                case 1:
                    changerEnemyState.SetActive(true);
                    break;
                case 2:
                    if (Instance.EnemyLevel == 5 || Instance.EnemyLevel == 6) {
                        SetActiveResult(Results.Win);
                    }
                    if (Instance.EnemyLevel == 9) { 
                        changerEnemyState.SetActive(true);
                    }
                    break;
                case 3:
                    SetActiveResult(Results.Win);
                    CharacterSelectManager.enemyState = 1;
                    break;

            }
        }

        if (!ChangeToNextState()) {
            SetActiveResult(Results.Win);
        }

    }
    void SetActiveResult(Results result) {
        CharacterSelectManager.results = result;
        resultText.text = result == Results.Lose ? "GAME OVER" : "VICTORY";
        gameOverMask.SetActive(result == Results.Lose);
        victoryMask.SetActive(result == Results.Win);

    }
    private void OnDisable() {
        CharacterSelectManager.enemyState = 1;
        attackbutton[5].interactable = true;
    }
}
