using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingMenuController : MonoBehaviour
{
    [SerializeField] private SharedInt enemyCount;
    [SerializeField] private SharedBool dayGoing;
    [SerializeField] private SharedInt dayCount;
    
    private void Awake()
    {
        dayGoing.valueChangeEvent.AddListener(CheckDayRunning);
        enemyCount.valueChangeEvent.AddListener(CheckEnemyCount);
    }

    private void Start()
    {
        dayGoing.Value = false;
    }

    public void DoneUpgrading()
    {
        dayCount.Value++;
        dayGoing.Value = true;
    }
    
    private void OnDestroy()
    {
        dayGoing.valueChangeEvent.RemoveListener(CheckDayRunning);
        enemyCount.valueChangeEvent.RemoveListener(CheckEnemyCount);
    }

    private void CheckDayRunning()
    {
        if (dayGoing.Value == false)
        {
            for(int i=0; i< transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if(child != null)
                    child.SetActive(true);
            }
        }
        else
        {
            for(int i=0; i< transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if(child != null)
                    child.SetActive(false);
            }
        }
    }

    private void CheckEnemyCount()
    {
        if (enemyCount.Value == 0 && dayGoing.Value == true)
        {
            dayGoing.Value = false;
        }
    }
}
