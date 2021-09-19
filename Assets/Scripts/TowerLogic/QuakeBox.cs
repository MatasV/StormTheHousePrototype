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
        
            //Debug.Log("Shake");
            foreach (var enemy in activeEnemies)
            {
                switch (enemy.enemyType)
                {
                    case Enemy.EnemyType.Foot:
                        enemy.OnHit(towerData.damage);
                        break;
                    case Enemy.EnemyType.Armored:
                        enemy.Trip();
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

            var slowUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Range);

            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
    }
}