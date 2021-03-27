using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Upgrades upgrades;
    [SerializeField] private SharedInt money;
    [SerializeField] private SharedInt houseHealth;
    [SerializeField] private SharedInt houseMaxHealth;
    private void Start()
    {
        upgrades.repairmen = 0;
        upgrades.shooters = 0;
        upgrades.wallLevel = 1;
        upgrades.onUpgraded.Invoke();
    }

    public void UpgradeRepairmen()
    {
       var price = (int)(upgrades.repairmenInitialPrice +
                         upgrades.repairmen * upgrades.repairmenIncreasePriceIncrement);
       
        if (money.Value >= price)
        {
            money.Value -= price;
            upgrades.repairmen++;
            upgrades.onUpgraded.Invoke();
        }
    }
    public void UpgradeShooters()
    {
        var price = (int)(upgrades.shootersInitialPrice + 
                          upgrades.shooters * upgrades.shootersIncreasePriceIncrement);
       
        if (money.Value >= price)
        {
            money.Value -= price;
            upgrades.shooters++;
            upgrades.onUpgraded.Invoke();
        }
    }
    public void UpgradeWall()
    {
        var price = (int)(upgrades.wallInitialPrice +
                          upgrades.wallLevel * upgrades.wallIncreasePriceIncrement);
       
        if (money.Value >= price)
        {
            money.Value -= price;
            upgrades.wallLevel++;
            upgrades.onUpgraded.Invoke();
        }
    }

    public void Heal()
    {
        if (houseHealth.Value < houseMaxHealth.Value && money.Value >= upgrades.houseHealPrice)
        {
            houseHealth.Value = Math.Min( houseHealth.Value + upgrades.houseHealAmount, houseMaxHealth.Value);
        }
    }
}

