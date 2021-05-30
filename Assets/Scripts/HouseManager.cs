using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [SerializeField] private HouseHealth houseHealth;
    [SerializeField] private SharedFloat houseX;
    [SerializeField] private Upgrade healthUpgrade;

    [SerializeField] private Upgrade repairmenUpgrade;
    [SerializeField] private SharedBool roundStarted;

    private const float HealTime = 3f;
    private float healTimer = 0f;
    private void Heal()
    {
        houseHealth.Value = Math.Min( houseHealth.Value + repairmenUpgrade.level, houseHealth.maxHealth.Value);
    }
    private void Update()
    {
        if (repairmenUpgrade.level == 0 || roundStarted.Value == false || houseHealth.Value == houseHealth.maxHealth.Value) return;
        
        healTimer += Time.deltaTime;
        if (healTimer >= HealTime)
        {
            healTimer = 0f;
            Heal();
        }
    }
    private void Start()
    {
        houseX.Value = transform.position.x;
        houseHealth.maxHealth.Value = healthUpgrade.level * 100;
        houseHealth.Value = houseHealth.maxHealth.Value;
        
        healthUpgrade.onUpgradeChanged.AddListener(SyncUpgrades);
    }
    
    private void SyncUpgrades()
    {
        houseHealth.maxHealth.Value = healthUpgrade.level * 100;
    }

    private void OnDestroy()
    {
        healthUpgrade.onUpgradeChanged.RemoveListener(SyncUpgrades);
    }
}