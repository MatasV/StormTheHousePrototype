using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text txtDisplay;
    [SerializeField] private Slider healthSlider;
    
    [SerializeField] private HouseHealth health;

    private void Awake()
    {
        health.valueChangeEvent.AddListener(DisplayHealth);
        health.maxHealth.valueChangeEvent.AddListener(DisplayMaxHealth);
    }

    private void Start()
    {
       
    }

    private void DisplayHealth()
    {
        healthSlider.value = Math.Max(health.Value, 0);
        txtDisplay.text = $"{(Math.Max((int)health.Value, 0)).ToString()}/{health.maxHealth.Value.ToString()}";
    }

    private void DisplayMaxHealth()
    {
        healthSlider.maxValue = health.maxHealth.Value;
        healthSlider.value = health.maxHealth.Value;
        txtDisplay.text = $"{(Math.Max((int)health.Value, 0)).ToString()}/{health.maxHealth.Value.ToString()}";
    }

    private void OnDestroy()
    {
        health.valueChangeEvent.RemoveListener(DisplayHealth);
        health.maxHealth.valueChangeEvent.RemoveListener(DisplayMaxHealth);
    }
}
