using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    public float timer;
    public Enemy enemy;

    public virtual void Tick()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f) DisableEffect();
    }

    public virtual void EnableEffect(Enemy _enemy)
    {
        if (enemy == null)
        {
            Debug.LogWarning("Enemy is Null, returning...");
        }
        else
        {
            enemy = _enemy;
        }
    }

    public abstract void DisableEffect();
}