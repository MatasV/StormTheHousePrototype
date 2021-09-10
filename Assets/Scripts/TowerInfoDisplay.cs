using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoDisplay : MonoBehaviour
{
    [SerializeField] private SharedInt money;

    [Header("Tower Purchase Info")] [SerializeField]
    private Image towerNotPurchasedSpriteImage;

    [SerializeField] private TMP_Text towerNotPurchasedCostText;
    [SerializeField] private TMP_Text towerNotPurchasedNameText;
    [SerializeField] private TMP_Text towerNotPurchasedDescriptionText;

    [SerializeField] private Button purchaseButton;

    [Header("Tower Upgrade Display")] [SerializeField]
    private Image towerPurchasedSpriteImage;

    [SerializeField] private TMP_Text towerPurchasedNameText;
    [SerializeField] private Transform towerDisplayTransform;
    [SerializeField] private Transform upgradeButtonsParent;
    [SerializeField] private GameObject upgradeUIObject;


    [SerializeField] private PurchasingPanelController purchasingPanelController;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        var towerPurchases = towerDisplayTransform.GetComponentsInChildren<TowerPurchase>();

        foreach (var gunPurchase in towerPurchases)
        {
            gunPurchase.onTowerSelected += DisplayNotPurchasedTowerInfo;
        }

        purchasingPanelController = GameObject.FindObjectOfType<PurchasingPanelController>();
    }

    private void DisplayNotPurchasedTowerInfo(TowerData tower)
    {
        if (purchasingPanelController == null) Debug.LogWarning("Purchasing panel controller not found");
        else purchasingPanelController.EnableTowerPurchasePanel();

        towerNotPurchasedSpriteImage.sprite = tower.mainSprite;
        towerNotPurchasedCostText.text = "$" + tower.costToPurchase.ToString();
        towerNotPurchasedNameText.text = tower.towerName;
        towerNotPurchasedDescriptionText.text = tower.description;

        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(() => Purchase(tower));
    }

    public void DisplayPurchasedTowerInfo(Tower tower)
    {
        if (purchasingPanelController == null) Debug.LogWarning("Purchasing panel controller not found");
        else purchasingPanelController.EnableTowerUpgradePanel();

        towerPurchasedSpriteImage.sprite = tower.towerData.mainSprite;
        towerPurchasedNameText.text = tower.towerData.towerName;

        var children = upgradeButtonsParent.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(upgradeButtonsParent.GetChild(i).gameObject);
        }

        for (var i = 0; i < tower.upgradeItemsList.Count; i++)
        {
            var upgradeUI = Instantiate(upgradeUIObject, upgradeButtonsParent).GetComponent<TowerUpgrade>();
            upgradeUI.Init(tower.upgradeItemsList[i]);
        }
    }

    public void Purchase(TowerData tower) //enter placement phase
    {
        if (money.Value >= tower.costToPurchase)
        {
            money.Value -= tower.costToPurchase;
        }
    }
}