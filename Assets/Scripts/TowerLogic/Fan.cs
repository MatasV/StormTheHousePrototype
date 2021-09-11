using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerLogic
{
    public class Fan : Tower
    {
        private EnemySpawner enemySpawner;
        [SerializeField] private BoxCollider2D fanRange;
        
        private float effectAddTime = 1f;
        private float effectAddTimer = 0f;
        public override void Shoot()
        {
            var colliders = new List<Collider2D>();
            fanRange.GetContacts(colliders);

            foreach (var col in colliders)
            {
                var enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.AddEffect(new Slowed() { timer = 5f});
                } 
            }
        }

        public void Update()
        {
            effectAddTimer += Time.deltaTime;
            if (effectAddTimer >= effectAddTime)
            {
                Shoot();
                effectAddTimer = 0f;
            }
        }
    
        public override void Init()
        {
            base.Init();

            var slowUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Range);
            
            slowUpgrade.onUpgraded +=
                item => fanRange.size = new Vector2(item.value, fanRange.size.y);

            fanRange.size = new Vector2(slowUpgrade.value, fanRange.size.y);
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        private void OnEnable()
        {
            base.Init();
            
            var slowUpgrade = upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Range);
            
            slowUpgrade.onUpgraded +=
                item => fanRange.size = new Vector2(item.value, fanRange.size.y);

            fanRange.size = new Vector2(slowUpgrade.value, fanRange.size.y);
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
        
    }
}