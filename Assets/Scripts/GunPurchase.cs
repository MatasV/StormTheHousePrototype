using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPurchase : MonoBehaviour
{
    [SerializeField] private Gun gun;

    public delegate void OnGunSelected(Gun gun);

    public OnGunSelected onGunSelected;

    public void SelectGun()
    {
        onGunSelected?.Invoke(gun);
    }

    private void Start()
    {
        gun?.Init();
    }

    private void OnDestroy()
    {
        gun?.Finalize();
    }
}
