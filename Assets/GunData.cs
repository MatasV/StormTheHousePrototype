using UnityEngine;

[CreateAssetMenu]
public class GunData : ScriptableObject
{
    public int currentAmmo;
    public float damage;
    public int maxAmmo;
    public int fireRate;
    public float reloadTime;
    public float accuracy;
}
