using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SharedGun : ScriptableObject
{
    [SerializeField] private Gun _value;
    public Gun Value
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