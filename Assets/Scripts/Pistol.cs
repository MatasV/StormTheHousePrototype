using UnityEngine;

[CreateAssetMenu(menuName = "Guns/Pistol")]
public class Pistol : Gun
{
    public override void Shoot(Vector3 position)
    {
        gunData.currentAmmo--;
        if(gunData.currentAmmo == 0) Reload();
        
        onShoot?.Invoke();
        Debug.Log("Pistol Shoot" + position);
        Collider2D[] results = Physics2D.OverlapPointAll(position, 1 << LayerMask.NameToLayer("Enemy"), -Mathf.Infinity, Mathf.Infinity);
        var size = results.Length;
        // If it hits something...
        Debug.Log(size);
        if (size == 0) return;
        
        foreach (var res in results)
        {
            Enemy foundEnemy;
            if ((foundEnemy = res.gameObject.GetComponent<Enemy>()) != null)
            {
                Debug.Log("Found enemy");
                
                foundEnemy?.OnHit(gunData.damage);
                break;
            }
        }
    }
    public override void Reload()
    {
        reloading = true;
        onReload?.Invoke();
        
        var go = new GameObject();
        var reloader = go.AddComponent<GunReloader>();
        reloader.ReloadDone.AddListener(ReloadDone);
        reloader.Reload(this);
    }

    public override void ReloadDone()
    {
        gunData.currentAmmo = gunData.maxAmmo;
        reloading = false;
    }

    private void SyncAccuracy(GunData.UpgradeableItem accuracyItem)
    {
        gunData.accuracy = accuracyItem.maxValue-accuracyItem.value;
    }

    private void SyncAmmo(GunData.UpgradeableItem ammoItem)
    {
        gunData.maxAmmo = (int)ammoItem.value;
    }

    private void SyncReload(GunData.UpgradeableItem reloadItem)
    {
        gunData.reloadTime = 3-reloadItem.value;
    }
    
    public override void Init()
    {
        base.Init();
        var accUpgrade =
            gunData.upgradeItemsList.Find(x => x.upgradeType == GunData.UpgradeableItem.UpgradeType.Accuracy);
        accUpgrade.onUpgraded += SyncAccuracy;

        SyncAccuracy(accUpgrade);
        
        var ammoUpgrade =
            gunData.upgradeItemsList.Find(x => x.upgradeType == GunData.UpgradeableItem.UpgradeType.Ammo);
        ammoUpgrade.onUpgraded += SyncAmmo;

        SyncAmmo(ammoUpgrade);
        
        var reloadUpgrade =
            gunData.upgradeItemsList.Find(x => x.upgradeType == GunData.UpgradeableItem.UpgradeType.Reload);
        reloadUpgrade.onUpgraded += SyncReload;

        SyncReload(reloadUpgrade);
    }

    public override void Finalize()
    {
        var accUpgrade =
        gunData.upgradeItemsList.Find(x => x.upgradeType == GunData.UpgradeableItem.UpgradeType.Accuracy);
        accUpgrade.onUpgraded -= SyncAccuracy;
        
        var ammoUpgrade =
            gunData.upgradeItemsList.Find(x => x.upgradeType == GunData.UpgradeableItem.UpgradeType.Ammo);
        ammoUpgrade.onUpgraded -= SyncAmmo;
        
        var reloadUpgrade =
            gunData.upgradeItemsList.Find(x => x.upgradeType == GunData.UpgradeableItem.UpgradeType.Reload);
        reloadUpgrade.onUpgraded -= SyncReload;
    }
}