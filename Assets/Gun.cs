using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class Gun : ScriptableObject
{
    public GunData gunData;
    public UnityEvent onReload = new UnityEvent();
    public UnityEvent onShoot = new UnityEvent();
    public bool reloading;
    public virtual void Shoot(Vector3 position)
    {
        Debug.Log(" Shootin");
    }

    public void Reload()
    {
        Debug.Log("Reload CALLED" + (int)gunData.reloadTime );
        reloading = true;
        onReload?.Invoke();
        
        var go = new GameObject();
        var reloader = go.AddComponent<GunReloader>();
        reloader.ReloadDone.AddListener(ReloadDone);
        reloader.Reload(this);
    }

    public void ReloadDone()
    {
        Debug.Log("Reload Done");
        gunData.currentAmmo = gunData.maxAmmo;
        reloading = false;
    }
}