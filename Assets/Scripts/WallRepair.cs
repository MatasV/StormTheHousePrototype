using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRepair : MonoBehaviour
{
    [SerializeField] private int repairCost;
    [SerializeField] private int repairAmount;
    [SerializeField] private SharedInt playerMoney;
    [SerializeField] private HouseHealth houseHealth;

    public void Heal()
    {
        if (playerMoney.Value >= repairCost && houseHealth.Value != houseHealth.maxHealth.Value)
        {
            houseHealth.Value = Math.Min(houseHealth.maxHealth.Value, houseHealth.Value + repairAmount);
            playerMoney.Value -= repairCost;
        }
        
    }
}
