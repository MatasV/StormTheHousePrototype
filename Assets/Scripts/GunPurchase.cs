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
        gun?.Init();
        GetComponent<Button>().onClick.AddListener(()=>onGunSelected.Invoke(gun));
    }

    private void OnDestroy()
    {
        gun?.Finalize();
        GetComponent<Button>().onClick.RemoveListener(()=>onGunSelected.Invoke(gun));
    }
}
