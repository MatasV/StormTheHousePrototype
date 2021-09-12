using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerLogic
{
    public class Tesla : Tower
    {
        private float shootTimer;
        
        [SerializeField] private BoxCollider2D fanRange;

        [SerializeField]
        private int shootTimeInitialValue = 200;
        private EnemySpawner enemySpawner;

        private Tower.UpgradeableItem fireRateUpgrade;
        private Tower.UpgradeableItem damageUpgrade;
        public override void Shoot()
        {
            var colliders = new List<Collider2D>();
            fanRange.GetContacts(colliders);

            foreach (var col in colliders)
            {
                var enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.OnHit(damageUpgrade.value);
                } 
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
            
            var rangeUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Range);
            
            rangeUpgrade.onUpgraded +=
                item => fanRange.size = new Vector2(item.value, fanRange.size.y);

            fanRange.size = new Vector2(rangeUpgrade.value, fanRange.size.y);
            
            fireRateUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.FireRate);
            damageUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Damage);

            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
        
        private void OnEnable()
        {
            base.Init();
            
            var rangeUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Range);
            
            rangeUpgrade.onUpgraded +=
                item => fanRange.size = new Vector2(item.value, fanRange.size.y);

            fanRange.size = new Vector2(rangeUpgrade.value, fanRange.size.y);
            
            fireRateUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.FireRate);
            damageUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Damage);

            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
    }
}