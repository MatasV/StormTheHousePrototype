using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgadeController : MonoBehaviour
{
    [SerializeField] private TMP_Text costDisplay;
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private int levelAtStart;
    private void Start()
    {
        upgrade.onUpgradeChanged.AddListener(ShowCost);
        upgrade.level = levelAtStart;
    }

    public void OnEnable()
    {
        ShowCost();
    }

    private void ShowCost()
    {
        costDisplay.text = "$" + upgrade.Cost;
    }

    private void OnDestroy()
    {
        upgrade.onUpgradeChanged.RemoveListener(ShowCost);
    }
}
