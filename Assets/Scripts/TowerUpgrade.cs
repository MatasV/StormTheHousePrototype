using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField] private Tower.UpgradeableItem item;
    [SerializeField] private SharedInt playerMoney;

    [Header("Display")] 
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text currentUpgradeValueText;
    [SerializeField] private Button UpgradeButton;
    public void Init(Tower.UpgradeableItem _item)
    {
        if (item != null) item.onUpgraded -= UpdateInfo;
        item = _item;
        item.onUpgraded += UpdateInfo;
        UpgradeButton.onClick.AddListener(UpgradeTower);
        UpdateInfo(item);
    }

    public void UpdateInfo(Tower.UpgradeableItem _)
    {
        costText.text = "$" + item.GetCostForNextLevel();
        upgradeNameText.text = item.upgradeType.ToString();
        currentUpgradeValueText.text = item.value.ToString(CultureInfo.InvariantCulture);
    }

    public void UpgradeTower()
    {
        item.Upgrade(playerMoney);
    }

    private void OnDestroy()
    {
        UpgradeButton.onClick.RemoveListener(UpgradeTower);
    }
}