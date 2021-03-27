using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunReloader : MonoBehaviour
{
    public void Reload(Gun gun)
    {
        Invoke(nameof(DoneReload), (int)gun.gunData.reloadTime);
    }

    public UnityEvent ReloadDone = new UnityEvent();

    private void DoneReload()
    {
        ReloadDone?.Invoke();
        Destroy(gameObject);
    }    
    
    private void OnDestroy()
    {
        ReloadDone.RemoveAllListeners();
    }
}
