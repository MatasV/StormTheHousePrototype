using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerLogic
{
    public class Flamer : Tower
    {
        private float shootTimer;

        [SerializeField]
        private int shootTimeInitialValue = 6;

        private EnemySpawner enemySpawner;
        [SerializeField] private BoxCollider2D range;
        
        private Tower.UpgradeableItem fireRateUpgrade;
        private Tower.UpgradeableItem damageUpgrade;
        public override void Shoot()
        {
            Debug.Log("shoot");
            var colliders = new Collider2D[1000];
            var howManyOverlappingColliders = Physics2D.GetContacts(range, colliders);

            for (var index = 0; index < howManyOverlappingColliders; index++)
            {
                var col = colliders[index];
                Debug.Log("found coll");
                var enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.AddEffect(new Flaming() {timer = 5f, damage = damageUpgrade.value});
                }
            }
        }

        public void Update()
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= (shootTimeInitialValue-fireRateUpgrade.value))
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
                item => range.size = new Vector2(item.value, range.size.y);

            range.size = new Vector2(rangeUpgrade.value, range.size.y);
            
            fireRateUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.FireRate);
            damageUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Damage);

            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        private void OnEnable()
        {
            base.Init();
            
            var rangeUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Range);
            
            rangeUpgrade.onUpgraded +=
                item => range.size = new Vector2(item.value, range.size.y);

            range.size = new Vector2(rangeUpgrade.value, range.size.y);
            
            fireRateUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.FireRate);
            damageUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Damage);

            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
    }
}