using System;
using UnityEngine;

[CreateAssetMenu(fileName = "VoidEventChannelSO", menuName = "SO/Events/VoidEventChannelSO")]
public class VoidEventChannelSO : ScriptableObject
{
    public Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
