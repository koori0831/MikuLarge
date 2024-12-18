using System.Collections.Generic;
using UnityEngine;

public enum StateName
{
    Idle, Move, Dead, Hit, Landing, Jump, Fall, Dash, DoubleJump, Attack,Wake, Melee, Seat,Reload,Shot,Phase1,Phase2,Phase3,Phase1End
}

[CreateAssetMenu(fileName = "EntityFSMSO", menuName = "SO/FSM/EntityFSM")]
public class EntityFSMSO : ScriptableObject
{
    public List<StateSO> states;
    private Dictionary<StateName, StateSO> _statesDictionary;

    public StateSO this[StateName stateName] => _statesDictionary.GetValueOrDefault(stateName);

    private void OnEnable()
    {
        if (states == null) return;
        
        _statesDictionary = new Dictionary<StateName, StateSO>();
        foreach (var state in states)    
        {
            _statesDictionary.Add(state.stateName, state);
        }
    }
}
