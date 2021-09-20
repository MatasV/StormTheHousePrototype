using UnityEngine;

[CreateAssetMenu(menuName = "Create TowerData", fileName = "TowerData", order = 0)]
public class TowerData : ScriptableObject
{
    public GameObject turretPrefab;
    public string towerName;
    public float damage;
    public int fireRate;
    public Sprite mainSprite;
    public int costToPurchase;
    public int baseSellPrice;
    public bool purchased;
    [TextArea(3,10)] public string description;
}