using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class HouseHealth : ScriptableObject
{
    private int _value;
    public SharedInt maxHealth;
    public SharedInt shieldHealth;
    public int Value
    {
        get => _value;
        set
        {
            if (shieldHealth.Value > 0)
            {
                shieldHealth.Value -= value;
                return;
            }
            _value = value;
            valueChangeEvent.Invoke();
        }
    }
    public UnityEvent valueChangeEvent = new UnityEvent();
}

