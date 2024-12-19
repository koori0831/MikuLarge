using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolEventChannelSO", menuName = "SO/Events/BoolEventChannelSO")]
public class BoolEventChannelSO : ScriptableObject
{
    public Action<bool> OnValueEvent;

    public void RaiseEvent(bool value)
    {
        OnValueEvent?.Invoke(value);
    }
}
