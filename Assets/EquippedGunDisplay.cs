using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedGunDisplay : MonoBehaviour
{
    [SerializeField] private SharedGun firstGun;
    [SerializeField] private Image gunDisplay;
    private void Awake()
    {
        firstGun.valueChangeEvent.AddListener(DisplayGun);
    }

    private void DisplayGun()
    {
        gunDisplay.sprite = firstGun.Value?.gunData.sprite;
    }

    private void OnDestroy()
    {
        firstGun.valueChangeEvent.RemoveListener(DisplayGun);
    }
}
