using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyType {Foot, Armored }
    public EnemyType enemyType;
    
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private SharedFloat houseX;
    [SerializeField] private HouseHealth houseHealth;
    [SerializeField] private SharedInt enemyCount;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Slider healthSlider;
    
    protected Vector3 locationToDamage = new Vector3();
    private Vector3 startingPosition;
    private float journeyLength;
    private float startTime;
    private float health;

    private bool moving = false;

    public readonly List<Tuple<StatusEffect, GameObject>> statusEffects = new List<Tuple<StatusEffect, GameObject>>();
    
    public virtual void Init()
    {
        enemyCount.Value++;
        locationToDamage = new Vector3(houseX.Value - enemyData.distanceFromHouseToAttack , transform.position.y, transform.position.z);
        startingPosition = transform.position;
        journeyLength = Vector3.Distance(startingPosition, locationToDamage);
        startTime = Time.time;
        moving = true;
        health = enemyData.health;
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void AddEffect(StatusEffect effect)
    {
        Debug.Log($"adding effect {effect.GetType()}");
        foreach (var statusEffect in statusEffects)
        {
            if (statusEffect.GetType() == effect.GetType())
            {
                Debug.Log("effect found, extending");
                statusEffect.Item1.timer += effect.timer;
                return;
            }
        }
        
        Debug.Log("effect not found, adding");
        effect.EnableEffect(this);
        
        if (effect.statusObj != null)
        {
            var obj = Instantiate(effect.statusObj);
            statusEffects.Add(new Tuple<StatusEffect, GameObject>(effect, obj));
        }
        else
        {
            statusEffects.Add(new Tuple<StatusEffect, GameObject>(effect, null));
        }
        
        
        
    }
    private void Update()
    {
        if (!moving) return;
        
        var distCovered = (Time.time - startTime) * enemyData.speed;
        var fractionOfJourney = distCovered / journeyLength;
        
        rb.position = Vector3.Lerp(startingPosition, locationToDamage, fractionOfJourney);

        if (rb.position.Equals(locationToDamage))
        {
            moving = false;
            InvokeRepeating(nameof(DamageHouse), 0f, enemyData.fireRate);
        }

        //Ticking Effects
        for (var index = statusEffects.Count -1; index >= 0 ; index--)
        {
            Debug.Log("found effect ticking");
            var statusEffect = statusEffects[index];
            statusEffect.Item1.Tick();
            if (statusEffect.Item1.timer <= 0)
            {
                statusEffect.Item1.DisableEffect();
                if (statusEffect.Item2 != null)
                {
                    Destroy(statusEffect.Item2);
                }
                statusEffects.Remove(statusEffect); //not sure if this is gonna work, I hope it will?
            }
        }
    }

    protected virtual void Die()
    {
        enemyCount.Value--;
        //Play Death Anim
        Destroy(gameObject);
    }

    public virtual void OnHit(float damage)
    {
        Debug.Log("i GOT HIT!");
        healthSlider.value = health;
        if ((health -= damage) <= 0f)
        {
            Die();
        }
    }

    public virtual void DamageHouse()
    {
        houseHealth.Value -= enemyData.damage;
    }

    public virtual void Trip()
    {
        Debug.Log("Tripped");
    }
}