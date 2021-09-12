using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Tower : MonoBehaviour
{
    [FormerlySerializedAs("turretData")] public TowerData towerData;

    [Serializable]
    public class UpgradeableItem
    {
        public enum UpgradeType {Damage, Reload, Ammo, Range, Shield, Missiles, Slow, FireRate};

        public UpgradeType upgradeType;
        public float value;
        public float startingValue;
        public float valueIncrement;
        public float maxValue;

        public int timesUpgraded = 0;
        
        public int startingCost;
        public float costIncrement;
        public delegate void OnUpgrade(UpgradeableItem item);
        public OnUpgrade onUpgraded;
        
        public int GetCostForNextLevel()
        {
            if (timesUpgraded == 0) return startingCost;
            else return (int)(startingCost * (timesUpgraded * costIncrement));
        }
        
        public void Upgrade(SharedInt money)
        {
            if (money.Value >= (int) value && value < maxValue)
            {
                money.Value -= GetCostForNextLevel();
                value += valueIncrement;
                timesUpgraded++;
                onUpgraded?.Invoke(this);
            }
        }

        public void Init()
        {
            value = startingValue;
            Debug.Log("initing upgrade");
        }
    }

    public List<UpgradeableItem> upgradeItemsList = new List<UpgradeableItem>();
    
    public virtual void Init()
    {
        foreach (var item in upgradeItemsList)
        {
            item.Init();
        }
    }

    public abstract void Shoot();
}