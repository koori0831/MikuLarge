using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoEventChannelSO", menuName = "SO/Events/SoEventChannelSO")]
public class PlayerSaveSoEventChannelSO : ScriptableObject
{
    public Action<PlayerSavesSO> OnEventRaised;

    public void RaiseEvent(PlayerSavesSO value)
    {
        OnEventRaised?.Invoke(value);
    }
}
