using System;

using UnityEngine;
using UnityEngine.Events;



public abstract class Gun : ScriptableObject
{
    public GunData gunData;
    private GunData _initialGunData;
    
    public UnityEvent onReload = new UnityEvent();
    public UnityEvent onShoot = new UnityEvent();
    public bool reloading;

    public virtual void Init() //Reset GunData Values
    {
        gunData.purchased = gunData.purchasedAtStart;
        foreach (var item in gunData.upgradeItemsList)
        {
            item.Init();
        }
    }

    public abstract void Shoot(Vector3 position);

    public virtual void Reload()
    {
        Debug.Log("Reload CALLED" + (int)gunData.reloadTime );
        reloading = true;
        onReload?.Invoke();
        
        var go = new GameObject();
        var reloader = go.AddComponent<GunReloader>();
        reloader.ReloadDone.AddListener(ReloadDone);
        reloader.Reload(this);
    }

    public virtual void ReloadDone()
    {
        Debug.Log("Reload Done");
        gunData.currentAmmo = gunData.maxAmmo;
        reloading = false;
    }

    public abstract void Finalize();
}