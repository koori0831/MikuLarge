using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityEventChannelSO", menuName = "SO/Events/EntityEventChannelSO")]
public class EntityHealthEventChannelSO : ScriptableObject
{
    public Action<EntityHealth> OnEventRaised;

    public void RaiseEvent(EntityHealth value)
    {
        OnEventRaised?.Invoke(value);
    }
}
