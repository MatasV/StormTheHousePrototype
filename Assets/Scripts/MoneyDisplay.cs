using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private SharedInt playerMoney;
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        playerMoney.valueChangeEvent.AddListener(DisplayMoney);
    }

    private void DisplayMoney()
    {
        text.text = "$"+playerMoney.Value.ToString();
    }

    private void OnDestroy()
    {
        playerMoney.valueChangeEvent.RemoveListener(DisplayMoney);
    }
}

