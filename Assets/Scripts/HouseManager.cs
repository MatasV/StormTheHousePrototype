using System;
using System.Collections;
using System.Collections.Generic;
using TowerLogic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [SerializeField] private HouseHealth houseHealth;
    [SerializeField] private SharedFloat houseX;
    [SerializeField] private Upgrade healthUpgrade;

    [SerializeField] private Upgrade repairmenUpgrade;
    [SerializeField] private SharedBool roundStarted;
    [SerializeField] private SharedVoid towerSetupUpdated;

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
    }
    
    private void SyncUpgrades()
    {
        houseHealth.maxHealth.Value = healthUpgrade.level * 100;
        houseHealth.Value = houseHealth.maxHealth.Value;
    }

    private void OnDestroy()
    {
        healthUpgrade.onUpgradeChanged.RemoveListener(SyncUpgrades);
        roundStarted.valueChangeEvent.RemoveListener(UpdateTowers);
        towerSetupUpdated.valueChangeEvent.RemoveListener(UpdateTowers);
    }

    private void Awake()
    {
        healthUpgrade.onUpgradeChanged.AddListener(SyncUpgrades);
        roundStarted.valueChangeEvent.AddListener(UpdateTowers);
        towerSetupUpdated.valueChangeEvent.AddListener(UpdateTowers);
    }
    private void UpdateTowers()
    {
        SetupShieldHealth();
    }
    private void SetupShieldHealth()
    {
        if (roundStarted.Value == false) return;
        
        Debug.Log("Setting up shield");
        houseHealth.shieldLevel.Value = 0;

        var shields = FindObjectsOfType<ShieldGenerator>();
        
        for (var i = 0; i < shields.Length; i++)
        {
            var shieldGen = shields[i];

            houseHealth.shieldLevel.Value += (int)shieldGen.upgradeItemsList
                .Find(x => x.upgradeType == Tower.UpgradeableItem.UpgradeType.Shield).value / (i + 1);
        }
    }
}