using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunInfoDisplay : MonoBehaviour
{
    
    [SerializeField] private Image gunSpriteImage;
    [SerializeField] private TMP_Text gunCostText;
    [SerializeField] private TMP_Text gunNameText;
    
    [Header("Upgrade Display")]
    [SerializeField] private Transform gunDisplayTransform;
    [SerializeField] private Transform upgradeButtonsParent;
    [SerializeField] private GameObject upgradeUIObject;
    
    
    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        var gunPurchases = gunDisplayTransform.GetComponentsInChildren<GunPurchase>();

        foreach (var gunPurchase in gunPurchases)
        {
            gunPurchase.onGunSelected += DisplayGunInfo;
        }
    }

    private void DisplayGunInfo(Gun gun)
    {
        gunSpriteImage.sprite = gun.gunData.sprite;
        gunCostText.text = gun.gunData.costToPurchase.ToString();
        gunNameText.text = gun.gunData.gunName;

        var children = upgradeButtonsParent.GetComponentsInChildren<GunUpgrade>();
        for (var index = 0; index < children.Length; index++)
        {
            var gunUpgrade = children[index];
            gunUpgrade.UpdateInfo(gun.gunData.upgradeItemsList[index]);
        }
    }

    public void Purchase()
    {
        
    }
    
}
