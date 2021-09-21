using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShieldDisplay : MonoBehaviour
{
    [SerializeField] private HouseHealth houseHealth;

    private int previousShieldValue = 0;
    private int maxValue;

    [SerializeField] private TMP_Text displayText;
    private void Awake()
    {
        houseHealth.shieldHealth.valueChangeEvent.AddListener(SetupHealthDisplay);
    }

    private void SetupHealthDisplay()
    {
        if (previousShieldValue > houseHealth.shieldHealth.Value)
        {
            var currentValuePercentage = houseHealth.shieldHealth.Value / maxValue * 100 ;

            displayText.text = $"Shield: {currentValuePercentage}%";
        }
        else
        {
            maxValue = houseHealth.shieldHealth.Value;
        }
    }

    private void OnDestroy()
    {
        houseHealth.shieldHealth.valueChangeEvent.RemoveListener(SetupHealthDisplay);
    }
}
