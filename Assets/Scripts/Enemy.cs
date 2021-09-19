using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    [Serializable]
    public struct ActiveEffectData
    {
        public StatusEffect effect;
        public GameObject visualEffect;
    }
    
    public List<ActiveEffectData> statusEffects = new List<ActiveEffectData>();
    
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
            if (statusEffect.effect.GetType() == effect.GetType())
            {
                Debug.Log("effect found, extending");
                statusEffect.effect.timer += effect.timer;
                return;
            }
        }
        
        Debug.Log("effect not found, adding");
        effect.EnableEffect(this);
        
        //spawn status effect obj
        if (effect.statusObj != null)
        {
            var obj = Instantiate(effect.statusObj);
            statusEffects.Add(new ActiveEffectData(){ effect = effect, visualEffect = obj});
        }
        else
        {
            statusEffects.Add(new ActiveEffectData(){ effect = effect, visualEffect = null});
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
            Debug.Log($"found effect ticking {gameObject.name}");
            var statusEffect = statusEffects[index];
            statusEffect.effect.Tick();
            if (statusEffect.effect.timer <= 0)
            {
                Debug.Log("disabling effect");
                statusEffect.effect.DisableEffect();
                if (statusEffect.visualEffect != null)
                {
                    Destroy(statusEffect.visualEffect);
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