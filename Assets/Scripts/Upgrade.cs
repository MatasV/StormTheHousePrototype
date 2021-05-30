using System;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu]
public class Upgrade : ScriptableObject
{
    public string upgradeName;
    public Sprite upgradeImage;
    
    [SerializeField] private SharedInt playerMoney;
    
    public int level;
    
    [SerializeField] private float initialPrice;
    [SerializeField] private float priceIncrement;
    public float Cost => level * priceIncrement + initialPrice;
    public void UpgradeLevel() {
        Debug.Log("Trying to upgrade " + name);
        if (playerMoney.Value >= Cost)
        {
            level++;
            playerMoney.Value -= (int)Cost;
            onUpgradeChanged.Invoke();
        }
    }
    
    public UnityEvent onUpgradeChanged;
}

