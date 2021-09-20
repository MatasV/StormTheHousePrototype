using System;
using UnityEngine;

namespace TowerLogic
{
    public class QuakeBox : Tower
    {
        private float shootTimer;

        [SerializeField]
        private int shootTimeInitialValue = 200;

        private EnemySpawner enemySpawner;

        public override void Shoot()
        {
            var activeEnemies = enemySpawner.GetActiveEnemies();
        
            CameraShake.instance.ShakeCamera(0.1f, 0.05f);
            foreach (var enemy in activeEnemies)
            {
                switch (enemy.enemyType)
                {
                    case Enemy.EnemyType.Foot:
                        enemy.Trip();
                        break;
                    case Enemy.EnemyType.Armored:
                        enemy.OnHit(towerData.damage);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
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
            enemySpawner = GetComponent<EnemySpawner>();
        }

        private void OnEnable()
        {
            base.Init();
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
    }
}