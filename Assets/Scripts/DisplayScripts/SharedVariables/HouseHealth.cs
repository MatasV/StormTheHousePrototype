using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class HouseHealth : ScriptableObject
{
    private int _value;
    public SharedInt maxHealth;
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