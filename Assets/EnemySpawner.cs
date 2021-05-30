using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider2D))]
public class EnemySpawner : MonoBehaviour
{
    public List<EnemyWaveData> days = new List<EnemyWaveData>();
    public SharedInt dayIndex;
    private Collider2D col;
    [SerializeField] private SharedBool dayGoing;
    [SerializeField] private SharedInt enemyCount;
    [SerializeField] private Upgrade shootersUpgrade;

    private float shootTimer = 0f;
    private float shootTime;
    private void CalculateShootingSpeed()
    {
        shootTime = 500f / shootersUpgrade.level / 60f;
    }
    private void Shoot()
    {
        var enemies = transform.GetComponentsInChildren<Enemy>();
        if (enemies == null) return;
        
        enemies[Random.Range(0, enemies.Length)].OnHit(10);
        //Show shooting Animation;
    }
    private void Update()
    {
        if (shootersUpgrade.level == 0 || dayGoing.Value == false) return;
        
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootTime)
        {
            shootTimer = 0f;
            Shoot();
        }
    }
        
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        dayGoing.valueChangeEvent.AddListener(StartWave);
        shootersUpgrade.onUpgradeChanged.AddListener(CalculateShootingSpeed);
    }

    private void Start()
    {
        dayIndex.Value = -1;
    }

    private void StartWave()
    {
        if (dayGoing.Value)
        {
            if (dayIndex.Value >= days.Count) return; //Win!
            StartCoroutine(BeginWave());
        }
        else
        {
            enemyCount.Value = 0;
        }
    }

    private void OnDestroy()
    {
        dayGoing.valueChangeEvent.RemoveListener(StartWave);
        shootersUpgrade.onUpgradeChanged.RemoveListener(CalculateShootingSpeed);
    }

    private IEnumerator BeginWave()
    {
        if (days[dayIndex.Value] == null) yield return null;

        var enemyIndex = 0;
        var timeBetweenSpawning = days[dayIndex.Value].waveTime /
                                  days[dayIndex.Value].enemies.Count;
        
        while (enemyIndex < days[dayIndex.Value].enemies.Count)
        {
            var enemyData = days[dayIndex.Value].enemies[enemyIndex];
            
            if (enemyData != null && enemyData.enemyPrefab != null)
            {
                var enemyClone = Instantiate(enemyData.enemyPrefab, GetSpawnPoint(), Quaternion.identity, this.transform).GetComponent<Enemy>();

                if (enemyClone != null) enemyClone.Init();
                else
                {
                    Destroy(enemyClone.gameObject);
                    Debug.LogError($"Could not get Enemy script from spawned Enemy gameobject in enemy: {enemyIndex.ToString()} check prefab!");
                }
            }

            enemyIndex++;
            yield return new WaitForSeconds(timeBetweenSpawning);
        }
    }

    private Vector2 GetSpawnPoint()
    {
        var bounds = col.bounds;
        var center = bounds.center;

        var x = UnityEngine.Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
        var y = UnityEngine.Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);

        return new Vector2(x, y);
    }
}
