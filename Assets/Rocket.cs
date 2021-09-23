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
    
    public void Init(Vector2 targetPos, Vector2 startingPos)
    {
        target = targetPos;
        start = startingPos;

        startTime = Time.time;
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
        
        //transform.rotation =    
    }
}
