using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerLogic
{
    public class ShieldGenerator : Tower
    {
        private EnemySpawner enemySpawner;

        private Tower.UpgradeableItem fireRateUpgrade;
        private Tower.UpgradeableItem damageUpgrade;

        public void Update()
        {
        }
    
        public override void Init()
        {
            base.Init();

            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        public override void Shoot()
        {
            
        }

        private void OnEnable()
        {
            base.Init();

            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
    }
}