using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SharedInt houseHealth;
    [SerializeField] private SharedInt houseMaxHealth;
    
    [SerializeField] private SharedFloat houseX;

    [SerializeField] private Upgrades upgrades;
    private void Start()
    {
        houseX.Value = transform.position.x;
        houseMaxHealth.Value = upgrades.wallLevel * 100;
        houseHealth.Value = houseMaxHealth.Value;
        
        upgrades.onUpgraded.AddListener(SyncUpgrades);
    }

    private void SyncUpgrades()
    {
        houseMaxHealth.Value = upgrades.wallLevel * 100;
    }

    private void OnDestroy()
    {
        upgrades.onUpgraded.RemoveListener(SyncUpgrades);
    }
}