using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text repairmenDisplay;
    [SerializeField] private TMP_Text shooterDisplay;

    [SerializeField] private Upgrade repairmenUpgrade;
    [SerializeField] private Upgrade shooterUpgrade;

    private void Awake()
    {
        repairmenUpgrade.onUpgradeChanged.AddListener(DisplayRepairmen);
        shooterUpgrade.onUpgradeChanged.AddListener(DisplayShooters);
    }

    private void Start()
    {
        DisplayRepairmen();
        DisplayShooters();
    }

    private void DisplayRepairmen()
    {
        repairmenDisplay.text = "Repairmen: "+repairmenUpgrade.level.ToString();
    }

    private void DisplayShooters()
    {
        shooterDisplay.text = "Shooters: "+shooterUpgrade.level.ToString();
    }
    
    private void OnDestroy()
    {
        repairmenUpgrade.onUpgradeChanged.RemoveListener(DisplayRepairmen);
        shooterUpgrade.onUpgradeChanged.RemoveListener(DisplayShooters);
    }
}
