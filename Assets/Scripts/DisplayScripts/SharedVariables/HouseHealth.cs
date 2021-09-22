using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class HouseHealth : ScriptableObject
{
    private int _value;
    public SharedInt maxHealth;
    [FormerlySerializedAs("shieldHealth")] public SharedInt shieldLevel;
    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            valueChangeEvent.Invoke();
        }
    }
    public UnityEvent valueChangeEvent = new UnityEvent();
}