using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Upgrades : ScriptableObject
{
    public int wallLevel = 0;
    public int shooters = 0;
    public int repairmen = 0;

    public float wallIncreasePriceIncrement;
    public float wallInitialPrice;

    public float shootersIncreasePriceIncrement;
    public float shootersInitialPrice;

    public float repairmenIncreasePriceIncrement;
    public float repairmenInitialPrice;

    public float houseHealPrice;
    public int houseHealAmount;
    
    public UnityEvent onUpgraded = new UnityEvent();

}