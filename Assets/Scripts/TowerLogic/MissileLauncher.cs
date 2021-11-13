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
        
        private Tower.UpgradeableItem fireRateUpgrade;
        private Tower.UpgradeableItem damageUpgrade;
        private Tower.UpgradeableItem missileUpgrade;
        public override void Shoot()
        {
            var activeEnemies = enemySpawner.GetActiveEnemies();

            for (int i = 0; i < missileUpgrade.value; i++)
            {
                if (activeEnemies.Length < 1) return;

                var targetEnemy = activeEnemies[Random.Range(0, activeEnemies.Length - 1)];
                
                Debug.Log(targetEnemy.name);
                var rocketObj = Instantiate(rocketObject, transform.position, Quaternion.identity);
                rocketObj.GetComponent<Rocket>().Init(targetEnemy.transform.position, damageUpgrade.value);
            }
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
            enemySpawner = FindObjectOfType<EnemySpawner>();

            fireRateUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.FireRate);
            damageUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Damage);
            missileUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Missiles);
        }

        private void OnEnable()
        {
            base.Init();
            enemySpawner = FindObjectOfType<EnemySpawner>();

            fireRateUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.FireRate);
            damageUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Damage);
            missileUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Missiles);
        }
    }
}