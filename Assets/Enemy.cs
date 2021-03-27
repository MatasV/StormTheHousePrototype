using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private SharedFloat houseX;
    [SerializeField] private SharedInt houseHealth;
    
    protected Vector3 locationToDamage = new Vector3();
    private Vector3 startingPosition;
    private float journeyLength;
    private float startTime;
    private float health;

    private bool moving = false;

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        locationToDamage = new Vector3(houseX.Value - enemyData.distanceFromHouseToAttack , transform.position.y, transform.position.z);
        startingPosition = transform.position;
        journeyLength = Vector3.Distance(startingPosition, locationToDamage);
        startTime = Time.time;
        moving = true;
    }

    private void Update()
    {
        if (!moving) return;
        
        var distCovered = (Time.time - startTime) * enemyData.speed;
        var fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startingPosition, locationToDamage, fractionOfJourney);

        if (transform.position.Equals(locationToDamage))
        {
            moving = false;
            InvokeRepeating(nameof(DamageHouse), 0f, enemyData.fireRate);
        }
    }

    protected virtual void Die()
    {
        //Play Death Anim
        Destroy(gameObject);
    }

    public virtual void OnHit(float damage)
    {
        Debug.Log("i GOT HIT!");
        if ((health -= damage) <= 0f)
        {
            Die();
        }
    }

    public virtual void DamageHouse()
    {
        //Play Damage Anim
        houseHealth.Value -= enemyData.damage;
        
    }
}