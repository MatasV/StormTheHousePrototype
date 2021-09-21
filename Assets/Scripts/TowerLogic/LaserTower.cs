using System;
using System.Collections;
using UnityEngine;

namespace TowerLogic
{
    public class LaserTower : Tower
    {
        private float shootTimer;

        [SerializeField]
        private int shootTimeInitialValue = 200;
        private EnemySpawner enemySpawner;
        
        private Tower.UpgradeableItem fireRateUpgrade;
        private Tower.UpgradeableItem damageUpgrade;
        
        [SerializeField] private LineRenderer lineRenderer;
        public override void Shoot()
        {
            var activeEnemies = enemySpawner.GetActiveEnemies();

            if (activeEnemies.Length > 0)
            {
                var strongestEnemy = activeEnemies[0];
                foreach (var enemy in activeEnemies)
                {
                    if (enemy.health > strongestEnemy.health)
                    {
                        strongestEnemy = enemy;
                    }
                }

                StartCoroutine(ZapEnemy(strongestEnemy, damageUpgrade.value));
            }
        }

        private IEnumerator ZapEnemy(Enemy enemy, float damage)
        {
            Debug.Log($"attacking post { damage} ");
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;

            var teslaPosition = transform.position;
            teslaPosition.z = -3;

            var enemyPos = enemy.transform.position;
            enemyPos.z = -3;
            
            lineRenderer.SetPositions(new[] {teslaPosition, enemyPos});
            
            for (int i = 0; i < 75; i++)
            {
                yield return null;
            }
            enemy.OnHit(damage);
            CameraShake.instance.ShakeCamera(0.05f, 0.03f);
            lineRenderer.enabled = false;
            yield return null;
        }
        
        
        public void Update()
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= (shootTimeInitialValue-fireRateUpgrade.value)/60f) //turning to seconds
            {
                shootTimer = 0;
                Shoot();
            }
        }
    
        public override void Init()
        {
            base.Init();

            fireRateUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.FireRate);
            damageUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Damage);
            
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        public void OnEnable()
        {
            base.Init();

            fireRateUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.FireRate);
            damageUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Damage);
            
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
    }
}