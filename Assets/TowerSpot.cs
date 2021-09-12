using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    public Tower placedTower;

    public bool IsSpotEmpty()
    {
        return placedTower == null;
    }
}
