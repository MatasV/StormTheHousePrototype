using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunUpgrade : MonoBehaviour
{
    [SerializeField] private GunData.UpgradeableItem item;
    [SerializeField] private SharedInt playerMoney;

    [Header("Display")] 
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text currentUpgradeValueText;
    [SerializeField] private Button UpgradeButton;
    public void Init(GunData.UpgradeableItem _item)
    {
        if (item != null) item.onUpgraded -= UpdateInfo;
        item = _item;
        item.onUpgraded += UpdateInfo;
        UpgradeButton.onClick.AddListener(UpgradeGun);
        UpdateInfo(item);
    }

    public void UpdateInfo(GunData.UpgradeableItem _)
    {
        costText.text = "$" + item.GetCostForNextLevel();
        upgradeNameText.text = item.upgradeType.ToString();
        currentUpgradeValueText.text = item.value.ToString(CultureInfo.InvariantCulture);
    }

    public void UpgradeGun()
    {
        item.Upgrade(playerMoney);
    }

    private void OnDestroy()
    {
        UpgradeButton.onClick.RemoveListener(UpgradeGun);
    }
}