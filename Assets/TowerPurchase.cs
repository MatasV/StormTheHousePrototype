using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPurchase : MonoBehaviour
{
    [SerializeField] private TowerData tower;
    
    public delegate void OnTowerSelected(TowerData tower);
    public OnTowerSelected onTowerSelected;
    
    private void Start()
    {
        if (tower == null) return;
        GetComponent<Button>().onClick.AddListener(()=>onTowerSelected.Invoke(tower));
    }

    private void OnDestroy()
    {
        if (tower == null) return;
        GetComponent<Button>().onClick.RemoveListener(()=>onTowerSelected.Invoke(tower));
    }

    public TowerData GetGun()
    {
        return tower;
    }
}
