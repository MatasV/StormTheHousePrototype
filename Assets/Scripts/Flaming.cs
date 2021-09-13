using UnityEngine;

public class Flaming : StatusEffect
{
    public float damage;
    public Flaming()
    {
        statusObj = Resources.Load<GameObject>("StatusEffects/FlameEffect.prefab");
    }

    public override void Tick()  
    {
        base.Tick();
        enemy.OnHit(damage);
    }
    
    public override void DisableEffect() { }
}