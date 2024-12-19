using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IntEventChannelSO", menuName = "SO/Events/IntEventChannelSO")]
public class IntEventChannelSO : ScriptableObject
{
    public Action<int> OnValueEvent;
    
    public void RaiseEvent(int value)
    {
        OnValueEvent?.Invoke(value);    
    }
}
