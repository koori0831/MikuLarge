using UnityEngine;

public class LeviathanPhase1State : EntityState
{

    private Leviathan _leciahan;

    public LeviathanPhase1State(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _leciahan = entity as Leviathan;
    }

    public override void Enter()
    {
        base.Enter();
        _leciahan.AttackCompo.Attack();
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _leciahan.ChangeState(StateName.Idle);
        }
    }
}
