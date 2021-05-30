using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoDisplayText;
    [SerializeField] private Slider ammoSlider;

    [SerializeField] private SharedGun activeGun;
    private Gun previousGun;
    
    private float reloadTimer = 0f;
    private void Start()
    {
        activeGun.valueChangeEvent.AddListener(SyncGunInfo);
    }

    private void SyncAfterShot()
    {
        ammoDisplayText.text = $"{activeGun.Value.gunData.currentAmmo}/{activeGun.Value.gunData.maxAmmo}";
        ammoSlider.value = activeGun.Value.gunData.currentAmmo;
    }
    private void SyncGunInfo()
    {
        StopAllCoroutines();
        
        previousGun?.onReload.RemoveListener(Reload);
        previousGun?.onShoot.RemoveListener(SyncAfterShot);

        previousGun = activeGun.Value;
        
        ammoSlider.maxValue = activeGun.Value.gunData.maxAmmo;
        ammoSlider.value = activeGun.Value.gunData.currentAmmo;

        ammoDisplayText.text = $"{activeGun.Value.gunData.currentAmmo}/{activeGun.Value.gunData.maxAmmo}";
        activeGun.Value.onReload.AddListener(Reload);
        activeGun.Value.onShoot.AddListener(SyncAfterShot);
    }

    private void Reload()
    {
        reloadTimer = 0f;
        ammoSlider.maxValue = activeGun.Value.gunData.reloadTime;
        ammoSlider.value = 0;
        StartCoroutine(ReloadAnim());
    }

    private IEnumerator ReloadAnim()
    {
        while (reloadTimer < activeGun.Value.gunData.reloadTime)
        {
            ammoSlider.value = reloadTimer;
            reloadTimer += Time.deltaTime;
            yield return null;
        }
        
        reloadTimer = 0;
        
        ammoDisplayText.text =  $"{activeGun.Value.gunData.maxAmmo}/{activeGun.Value.gunData.maxAmmo}";
        
        ammoSlider.maxValue = activeGun.Value.gunData.maxAmmo;
        ammoSlider.value = activeGun.Value.gunData.currentAmmo;
    }
    private void OnDestroy()
    {
        activeGun.Value.onReload.RemoveListener(Reload);
        activeGun.Value.onShoot.RemoveListener(SyncAfterShot);
        activeGun.valueChangeEvent.RemoveListener(SyncGunInfo);
        
    }
}
