using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunData : ScriptableObject
{
    [Serializable]
    public class UpgradeableItem
    {
        public enum UpgradeType {Accuracy, Damage, Reload, Ammo, Radius};

        public UpgradeType upgradeType;
        public float value;
        public float startingValue;
        public float valueIncrement;
        public float maxValue;
        public delegate void OnUpgrade(UpgradeableItem item);
        public OnUpgrade onUpgraded;
        
        public int GetCostForNextLevel()
        {
            return (int) (startingValue + (value * valueIncrement));
        }
        
        public void Upgrade(SharedInt money)
        {
            if (money.Value >= (int) value && value < maxValue)
            {
                money.Value -= GetCostForNextLevel();
                value += valueIncrement;
                onUpgraded?.Invoke(this);
            }
        }

        public void Init()
        {
            value = startingValue;
        }
    }
    
    public List<UpgradeableItem> upgradeItemsList = new List<UpgradeableItem>();

    public string gunName;
    public int currentAmmo;
    public float damage;
    public int maxAmmo;
    public int fireRate;
    public float reloadTime;
    public float accuracy;
    public Sprite sprite;
    public int costToPurchase;
    public bool purchased;
    public bool purchasedAtStart;
    [TextArea(3,10)] public string description;
}
