using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunInfoDisplay : MonoBehaviour
{
    [SerializeField] private SharedGun firstGun;
    [SerializeField] private SharedGun secondGun;
    [SerializeField] private SharedInt money;

    [Header("Gun Purchased Info")] [SerializeField]
    private Image gunPurchasedSpriteImage;

    [SerializeField] private TMP_Text gunPurchasedNameText;
    [SerializeField] private GameObject purchasedInfoHolder;

    [Header("Gun Not Purchased Info")] [SerializeField]
    private Image gunNotPurchasedSpriteImage;

    [SerializeField] private TMP_Text gunNotPurchasedCostText;
    [SerializeField] private TMP_Text gunNotPurchasedNameText;
    [SerializeField] private TMP_Text gunNotPurchasedDescriptionText;
    [SerializeField] private GameObject notPurchasedInfoHolder;
    [SerializeField] private Button purchaseButton;

    [Header("Upgrade Display")] [SerializeField]
    private Transform gunDisplayTransform;

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
        if (!gun.gunData.purchased)
        {
            notPurchasedInfoHolder.SetActive(true);
            purchasedInfoHolder.SetActive(false);

            gunNotPurchasedSpriteImage.sprite = gun.gunData.sprite;
            gunNotPurchasedCostText.text = "$" + gun.gunData.costToPurchase.ToString();
            gunNotPurchasedNameText.text = gun.gunData.gunName;
            gunNotPurchasedDescriptionText.text = gun.gunData.description;

            purchaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.AddListener(() => Purchase(gun));
        }
        else
        {
            purchasedInfoHolder.SetActive(true);
            notPurchasedInfoHolder.SetActive(false);

            gunPurchasedSpriteImage.sprite = gun.gunData.sprite;
            gunPurchasedNameText.text = gun.gunData.gunName;

            var children = upgradeButtonsParent.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                Destroy(upgradeButtonsParent.GetChild(i).gameObject);
            }

            for (var i = 0; i < gun.gunData.upgradeItemsList.Count; i++)
            {
                var upgradeUI = Instantiate(upgradeUIObject, upgradeButtonsParent).GetComponent<GunUpgrade>();
                upgradeUI.Init(gun.gunData.upgradeItemsList[i]);
            }
        }
    }

    public void Purchase(Gun gun)
    {
        if (money.Value >= gun.gunData.costToPurchase)
        {
            money.Value -= gun.gunData.costToPurchase;

            secondGun.Value = firstGun.Value;
            firstGun.Value = gun;

            gun.gunData.purchased = true;
            
            DisplayGunInfo(gun);
        }
    }
}