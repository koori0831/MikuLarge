using UnityEngine;

[CreateAssetMenu(fileName = "StateSO", menuName = "SO/FSM/StateSO")]
public class StateSO : ScriptableObject
{
    public StateName stateName;
    public string className;
    public AnimParamSO animParam;
}
