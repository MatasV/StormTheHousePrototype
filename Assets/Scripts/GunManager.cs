using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GunManager : MonoBehaviour
{
    [SerializeField] private SharedGun firstGun;
    [SerializeField] private SharedGun secondGun;
    [SerializeField] private Transform target;
    
    [SerializeField] private Gun startingFirstGun;
    [SerializeField] private Gun startingSecondGun;
    
    private Camera mainCam;

    private float accuracyDecr = 1.0f;

    [SerializeField] private SharedBool isDayRunning;

    
    private void Start()
    {
        mainCam = Camera.main;
        ResetGuns();
    }

    private void ResetGuns()
    {
        if (startingFirstGun != null)
            firstGun.Value = startingFirstGun;
        else
            Debug.LogWarning("First Gun should not be set to null");
        
        secondGun.Value = startingSecondGun != null ? startingSecondGun : null;
    }

    private void OnDestroy()
    {
        isDayRunning.valueChangeEvent.RemoveListener(RoundStart);
        firstGun.valueChangeEvent.RemoveListener(()=>EquipFirstGun(firstGun.Value));
        secondGun.valueChangeEvent.RemoveListener(()=>EquipSecondGun(secondGun.Value));
    }

    private void Awake()
    {
        isDayRunning.valueChangeEvent.AddListener(RoundStart);
        firstGun.valueChangeEvent.AddListener(()=>EquipFirstGun(firstGun.Value));
        secondGun.valueChangeEvent.AddListener(()=>EquipSecondGun(secondGun.Value));
    }

    public void RoundStart()
    {
        if (firstGun != null && firstGun.Value != null)
        {
            firstGun.Value.gunData.currentAmmo = firstGun.Value.gunData.maxAmmo;
            firstGun.Value.reloading = false;
        }
        
        if (secondGun != null && secondGun.Value != null)
        {
            secondGun.Value.gunData.currentAmmo = secondGun.Value.gunData.maxAmmo;
            firstGun.Value.reloading = false;
        }
        
        firstGun.valueChangeEvent.Invoke();
    }

    public void EquipFirstGun(Gun gun)
    {
        if (gun == null) return;
        firstGun.Value.onReload.RemoveAllListeners();
    }

    public void EquipSecondGun(Gun gun)
    {
        if (gun == null) return;
    }

    private void Update()
    {
        if (isDayRunning.Value == false) return;
        var mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        target.position = mousePos;

        target.localScale = Vector3.one * accuracyDecr;
        accuracyDecr = Mathf.Max(accuracyDecr -= 0.0035f, 1f);
        
        
        if (!firstGun.Value.reloading && Input.GetMouseButtonDown(0))
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            
            obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            accuracyDecr += 0.1f;

            var shootingPosWithAccuracy =
                new Vector3(
                    mousePos.x + Random.Range(-firstGun.Value.gunData.accuracy, firstGun.Value.gunData.accuracy) /10f * accuracyDecr,
                    mousePos.y + Random.Range(-firstGun.Value.gunData.accuracy, firstGun.Value.gunData.accuracy) /10f * accuracyDecr,
                    mousePos.z
                );
            
            obj.transform.position = shootingPosWithAccuracy;
            firstGun.Value.Shoot(shootingPosWithAccuracy);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //Remove this
        {
            if(!firstGun.Value.reloading && secondGun.Value != null)
                EquipFirstGun(secondGun.Value);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!firstGun.Value.reloading && firstGun.Value != null)
            {
                firstGun.Value.Reload();
            }
        }
    }
    
}

