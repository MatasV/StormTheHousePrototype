using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingPanelController : MonoBehaviour
{
    [Header("Towers")]
    [SerializeField] private GameObject purchaseTowerInfoHolder;
    
    [Header("Guns")]
    [SerializeField] private GameObject gunPurchaseHolder;
    [SerializeField] private GameObject purchasedGunInfoHolder;

    public void EnableGunPurchasePanel()
    {
        DisableAllPanels();
        gunPurchaseHolder.SetActive(true);
    }

    public void EnableGunUpgradePanel()
    {
        DisableAllPanels();
        purchasedGunInfoHolder.SetActive(true);
    }
    public void DisableAllPanels()
    {
        purchaseTowerInfoHolder.SetActive(false);
        
        gunPurchaseHolder.SetActive(false);
        purchasedGunInfoHolder.SetActive(false);
    }

    public void EnableTowerPurchasePanel()
    {
        DisableAllPanels();
        purchaseTowerInfoHolder.SetActive(true);
    }

    public void EnableTowerUpgradePanel()
    {
        
    }
}
