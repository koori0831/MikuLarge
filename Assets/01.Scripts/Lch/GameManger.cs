using System;
using UnityEngine;

public class GameManger : MonoSingleton<GameManger>
{
    [SerializeField] public PlayerSavesSO SaveSo;
    [SerializeField] private PlayerSaveSoEventChannelSO _saveEvent;

    private void OnEnable()
    {
        _saveEvent.OnEventRaised += SavePlayer;
    }

    private void OnDestroy()
    {
        _saveEvent.OnEventRaised -= SavePlayer;
    }

    private void SavePlayer(PlayerSavesSO obj)
    {
        SaveSo = obj;
    }
}
