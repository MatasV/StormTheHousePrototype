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
}