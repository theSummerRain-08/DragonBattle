using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCurrency : MonoBehaviour {
    public static CurrentCurrency Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private float currentGold = 9000000;
    [SerializeField] private float curentBean = 20;

    public float previousGold;
    public void PreviousGoldUpdate() { 
        previousGold = currentGold;
    }

    public float CurrentGold => currentGold;
    public float CurentBean => curentBean;
    private void OnEnable() {
        if (!PlayerPrefs.HasKey("CurrentGold")) {
            PlayerPrefs.SetFloat("CurrentGold", 9000000);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("CurrentBean")) {
            PlayerPrefs.SetFloat("CurrentBean", 20);
            PlayerPrefs.Save();
        }
        currentGold = PlayerPrefs.GetFloat("CurrentGold", 0);
        curentBean = PlayerPrefs.GetFloat("CurrentBean", 0);
    }
    public void UpdateCurrentcy(Buff buff, float amount) {

        if (buff == Buff.Coin) {
            if (currentGold == 0 && amount < 0) return;
            currentGold += amount;
            PlayerPrefs.SetFloat("CurrentGold", currentGold);
            PlayerPrefs.Save();
        }

        if (buff == Buff.Bean) {
            if (curentBean == 0 && amount < 0)  return;
            curentBean += amount;
            PlayerPrefs.SetFloat("CurrentBean", curentBean);
            PlayerPrefs.Save();
        }
        
    }
    public bool CanUnlockItemWithPrice(float price) {
        if (currentGold >= price) {
            UpdateCurrentcy(Buff.Coin, -price);
            return true;
        } 
        else 
            return false;
        
    }
    public float levelCoin;
    public void UpdateLevelCoin(Results results) {
        if (results == Results.Win)
            levelCoin = 10 * (CharacterStats.Instance.EnemyLevel - 1);
        else levelCoin = 0;
    }
}
