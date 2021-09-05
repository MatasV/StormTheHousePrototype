using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EquippedGunDisplay : MonoBehaviour
{
    [FormerlySerializedAs("firstGun")] [SerializeField] private SharedGun gun;
    [SerializeField] private Image gunDisplay;
    private void Awake()
    {
        gun.valueChangeEvent.AddListener(DisplayGun);
    }

    private void DisplayGun()
    {
        if(gun.Value != null)
            gunDisplay.sprite = gun.Value.gunData.sprite;
    }

    private void OnDestroy()
    {
        gun.valueChangeEvent.RemoveListener(DisplayGun);
    }
}
