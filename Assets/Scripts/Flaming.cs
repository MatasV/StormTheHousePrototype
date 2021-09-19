using UnityEngine;

public class Flaming : StatusEffect
{
    public float damage;
    public Flaming(float _timer, float _damage)
    {
        statusObj = Resources.Load<GameObject>("StatusEffects/FlameEffect.prefab");
        timer = _timer;
        damage = _damage;
        Debug.Log($"Created flaming effect, damage {damage}, timer {timer}");
    }

    public override void Tick()
    {
        if (!enabled)
        {
            Debug.Log($"[FLAMING] effect not enabled");
            return;
        }
        base.Tick();
        Debug.Log($"damaging enemy {enemy.name}");
        enemy.OnHit(damage);
    }

    public override void DisableEffect()
    {
        Debug.Log("Disabling Effect");
    }
}