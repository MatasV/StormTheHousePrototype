using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class StatusEffect
{
    public float timer;
    public Enemy enemy;
    public GameObject statusObj;
    public bool enabled = false;
    public virtual void Tick()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f) DisableEffect();
    }

    public virtual void EnableEffect(Enemy _enemy)
    {
        if (_enemy == null)
        {
            Debug.LogWarning("Enemy is Null, returning...");
        }
        else
        {
            Debug.Log(_enemy +  " found");
            enemy = _enemy;
            enabled = true;
        }
    }

    public abstract void DisableEffect();
}