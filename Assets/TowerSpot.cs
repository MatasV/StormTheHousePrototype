using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    public Tower placedTower;

    public bool IsSpotEmpty()
    {
        return placedTower == null;
    }
    
    
}
