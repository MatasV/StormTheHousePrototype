using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunUpgrade : MonoBehaviour
{
    [SerializeField] private GunData.UpgradeableItem item;
    [SerializeField] private SharedInt playerMoney;

    [Header("Display")] 
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text upgradeNameText;
    public void UpdateInfo(GunData.UpgradeableItem _item)
    {
        if (item != null) item.onUpgraded -= UpdateInfo;
        
        item = _item;
        
        item.onUpgraded += UpdateInfo;
        
        //todo add stuff here
        
    }

    public void UpgradeGun()
    {
        
        
        UpdateInfo(item);
    }
}
