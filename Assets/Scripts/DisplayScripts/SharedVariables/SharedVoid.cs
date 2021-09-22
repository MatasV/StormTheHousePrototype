using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SharedVoid : ScriptableObject
{
    public UnityEvent valueChangeEvent = new UnityEvent();
}