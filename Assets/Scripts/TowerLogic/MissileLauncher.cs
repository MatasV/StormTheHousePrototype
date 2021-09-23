using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerLogic
{
    public class MissileLauncher : Tower
    {
        private float shootTimer;

        [SerializeField]
        private int shootTimeInitialValue = 200;

        [SerializeField] private GameObject rocketObject;
        private EnemySpawner enemySpawner;
        public override void Shoot()
        {
            var activeEnemies = enemySpawner.GetActiveEnemies();

            if (activeEnemies.Length < 1) return;

            var targetEnemy = activeEnemies[Random.Range(0, activeEnemies.Length - 1)];

            var rocketObj = Instantiate(rocketObject, transform.position, Quaternion.identity);
            rocketObj.GetComponent<Rocket>().Init(targetEnemy.transform.position, transform.position);
        }

        public void Update()
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= (shootTimeInitialValue-towerData.fireRate)/60f) //turning to seconds
            {
                shootTimer = 0;
                Shoot();
            }
        }
    
        public override void Init()
        {
            base.Init();
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        private void OnEnable()
        {
            base.Init();
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
    }
}