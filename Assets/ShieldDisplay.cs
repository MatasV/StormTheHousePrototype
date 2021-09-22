using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShieldDisplay : MonoBehaviour
{
    [SerializeField] private HouseHealth houseHealth;

    [SerializeField] private TMP_Text displayText;
    private void Awake()
    {
        houseHealth.shieldLevel.valueChangeEvent.AddListener(SetupShieldDisplay);
    }

    private void SetupShieldDisplay()
    {
        displayText.text = $"Shield: {houseHealth.shieldLevel.Value}%";
    }

    private void OnDestroy()
    {
        houseHealth.shieldLevel.valueChangeEvent.RemoveListener(SetupShieldDisplay);
    }
}
