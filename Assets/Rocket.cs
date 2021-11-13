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

    private Rigidbody2D rb;
    private Quaternion initialRotation;

    private float damageToDeal;
    public void Init(Vector2 targetPos, float _damageToDeal)
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        initialRotation = transform.rotation;
        damageToDeal = _damageToDeal;
        target = targetPos;
        Debug.Log(target);
        // think of it as top-down view of vectors: 
        //   we don't care about the y-component(height) of the initial and target position.
        Vector3 projectileXZPos = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 targetXZPos = new Vector3(targetPos.x, targetPos.y, 0);
    
        // rotate the object to face the target
        transform.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(45f * Mathf.Deg2Rad);
        float H = targetPos.y - transform.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)) );
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0,Vy,Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        rb.velocity = globalVelocity;
        
    }
    

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f,(Quaternion.LookRotation(rb.velocity) * initialRotation).z);
        
        if (Vector2.Distance(target, transform.position) < 0.2f)
        {
            Explode();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target);
    }

    private void Explode()
    {
        Debug.Log("Rocket Exploding");
        var colliders = new Collider2D[1000];
        var howManyOverlappingColliders = Physics2D.GetContacts(circleCollider2D, colliders);
Debug.Log(howManyOverlappingColliders);
        for (var index = 0; index < howManyOverlappingColliders; index++)
        {
            var col = colliders[index];
            var enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnHit(damageToDeal);
            }
        }
        
        Destroy(gameObject);
    }
}
