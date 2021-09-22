using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerLogic
{
    public class ShieldGenerator : Tower
    {
        private EnemySpawner enemySpawner;
        [SerializeField] private SharedVoid towerUpdatedEvent;
        public override void Init()
        {
            base.Init();
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        public override void Shoot() {}

        private void OnEnable()
        {
            base.Init();
            towerUpdatedEvent.valueChangeEvent.Invoke();

            upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Shield).onUpgraded +=
                item => towerUpdatedEvent.valueChangeEvent.Invoke(); 
            
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        private void OnDestroy()
        {
            upgradeItemsList.Find(x => x.upgradeType == UpgradeableItem.UpgradeType.Shield).onUpgraded -=
                item => towerUpdatedEvent.valueChangeEvent.Invoke(); 
        }
    }
}