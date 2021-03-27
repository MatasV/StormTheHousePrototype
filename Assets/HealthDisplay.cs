using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private int previousHealth = 0;

    [SerializeField] private TMP_Text txtDisplay;
    [SerializeField] private Slider healthSlider;
    
    [SerializeField] private SharedInt health;
    [SerializeField] private SharedInt maxHealth;
    private void Start()
    {
        health.valueChangeEvent.AddListener(DisplayHealth);
        maxHealth.valueChangeEvent.AddListener(DisplayMaxHealth);
        healthSlider.value = 0;
        DisplayHealth();
    }

    private void DisplayHealth()
    {
        healthSlider.value = Math.Max(health.Value, 0);
        txtDisplay.text = $"{((int)health.Value).ToString()}/{maxHealth.Value.ToString()}";
    }

    private void DisplayMaxHealth()
    {
        healthSlider.maxValue = health.Value;
        txtDisplay.text = $"{((int)health.Value).ToString()}/{maxHealth.Value.ToString()}";
    }

    private void OnDestroy()
    {
        health.valueChangeEvent.RemoveListener(DisplayHealth);
        maxHealth.valueChangeEvent.RemoveListener(DisplayMaxHealth);
    }
}
