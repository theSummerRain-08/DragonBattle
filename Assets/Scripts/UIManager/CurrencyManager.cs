using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] TextMeshProUGUI currentGold;
    [SerializeField] TextMeshProUGUI currentBean;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentBean.text = CurrentCurrency.Instance.CurentBean.ToString();
        if (CurrentCurrency.Instance.CurrentGold <= 9999999) {
            currentGold.text = CurrentCurrency.Instance.CurrentGold.ToString();
        } else currentGold.text = "10 000 000+";
    }
}
