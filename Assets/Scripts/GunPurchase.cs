using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPurchase : MonoBehaviour
{
    [SerializeField] private Gun gun;

    public delegate void OnGunSelected(Gun gun);
    public OnGunSelected onGunSelected;
    
    private void Start()
    {
        if (gun == null) return;
        gun.Init();
        gun.gunData.purchased = gun.gunData.purchasedAtStart;
        GetComponent<Button>().onClick.AddListener(()=>onGunSelected.Invoke(gun));
    }

    private void OnDestroy()
    {
        if (gun == null) return;
        gun.Finalize();
        GetComponent<Button>().onClick.RemoveListener(()=>onGunSelected.Invoke(gun));
    }
}
