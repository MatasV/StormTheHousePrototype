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
    [SerializeField] private Button sellButton;

    [Header("Tower Upgrade Display")] [SerializeField]
    private Image towerPurchasedSpriteImage;

    [SerializeField] private TMP_Text towerPurchasedNameText;
    [SerializeField] private Transform towerDisplayTransform;
    [SerializeField] private Transform upgradeButtonsParent;
    [SerializeField] private GameObject upgradeUIObject;

    [SerializeField] private PurchasingPanelController purchasingPanelController;

    [Header("Tower Placement/Selling Properties:")]
    [SerializeField] private LayerMask whatIsBuildingSpot;
    private GameObject towerToBuild;
    private int towerToBuildValue;
    private int baseTowerSellingValue;
    private Vector3 mousePosition;
    private bool buildingModeOn = false;
    private bool isSellingButtonPressed = false;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        FollowMouse();
        CheckSellingInput();
    }

    private void Setup()
    {
        var towerPurchases = towerDisplayTransform.GetComponentsInChildren<TowerPurchase>();

        foreach (var gunPurchase in towerPurchases)
        {
            gunPurchase.onTowerSelected += DisplayNotPurchasedTowerInfo;
        }

        purchasingPanelController = FindObjectOfType<PurchasingPanelController>();
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

        sellButton.onClick.RemoveAllListeners();
        sellButton.onClick.AddListener(() => Sell(tower));
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

    public void Sell(TowerData tower)
    {
        isSellingButtonPressed = true;
        baseTowerSellingValue = tower.baseSellPrice;
    }

    private void CheckSellingInput()
    {
        if (isSellingButtonPressed == true && Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(mousePosition, Vector2.zero, 100f, whatIsBuildingSpot);

            if (!hitInfo.transform.gameObject.GetComponent<TowerSpot>().IsSpotEmpty())
            {
                GameObject soldTower = hitInfo.collider.GetComponent<TowerSpot>().placedTower.gameObject;
                Destroy(soldTower);
                hitInfo.collider.GetComponent<TowerSpot>().placedTower = null;
                money.Value += baseTowerSellingValue;
                isSellingButtonPressed = false;
            }
        }
    }

    public void Purchase(TowerData tower)
    {
        if (money.Value >= tower.costToPurchase)
        {
            towerToBuild = Instantiate(tower.turretPrefab);
            towerToBuild.AddComponent<Tower>();
            tower.purchased = true;
            buildingModeOn = true;
            towerToBuildValue = tower.costToPurchase;
            money.Value -= towerToBuildValue;
        }
    }

    private void FollowMouse()
    {
        if (buildingModeOn != false && towerToBuild != null)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            towerToBuild.transform.position = Vector2.Lerp(transform.position, mousePosition, 1f);

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(mousePosition, Vector2.zero, 100f, whatIsBuildingSpot);

                if (hitInfo.collider != null && hitInfo.transform.gameObject.GetComponent<TowerSpot>().IsSpotEmpty())
                {
                    hitInfo.transform.gameObject.GetComponent<TowerSpot>().placedTower = towerToBuild.GetComponent<Tower>();
                    towerToBuild.transform.position = hitInfo.transform.position;
                    buildingModeOn = false;
                    towerToBuild = null;
                }
                else
                {
                    Destroy(towerToBuild);
                    buildingModeOn = false;
                    towerToBuild = null;
                    money.Value += towerToBuildValue;
                }
            }
        }
    }
}