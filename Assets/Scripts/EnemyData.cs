using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int damage;
    public float distanceFromHouseToAttack;
    public float speed;
    public float fireRate;
    public GameObject enemyPrefab;
}
