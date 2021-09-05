using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SharedBool : ScriptableObject
{
    private bool _value;
    public bool Value
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