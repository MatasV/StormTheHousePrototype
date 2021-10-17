 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Vector3 target;
    private Vector3 start;
    
    private float startTime;
    [SerializeField] private float journeyTime = 1f;

    private float damage;
    private CircleCollider2D circleCollider2D;

    private Vector3 previousPosition;
    public void Init(Vector2 targetPos, Vector2 startingPos, float damageToDeal)
    {
        target = targetPos;
        start = startingPos;

        startTime = Time.time;

        damage = damageToDeal;

        circleCollider2D = GetComponent<CircleCollider2D>();
        
    }
    

    private void Update()
    {
        Vector3 center = (target + start) * 0.5F;
        
        center -= new Vector3(0, 5, 0);
        
        Vector3 startRelCenter = start - center;
        Vector3 endRelCenter = target - center;
        
        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete);
        transform.position += center;
        
        
    }

    private void Explode()
    {
        Debug.Log("shoot");
        var colliders = new Collider2D[1000];
        var howManyOverlappingColliders = Physics2D.GetContacts(circleCollider2D, colliders);

        for (var index = 0; index < howManyOverlappingColliders; index++)
        {
            var col = colliders[index];
            var enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.AddEffect(new Flaming(5f, damage));
            }
        }
    }
}
