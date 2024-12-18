using UnityEngine;

public class LeviathanPhase2State : EntityState
{

    private Leviathan _leviathan;

    public LeviathanPhase2State(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _leviathan = entity as Leviathan;
    }

    public override void Enter()
    {
        base.Enter();
        _leviathan.AttackCompo.WaterArrowAttack();
    }

    public override void Update()
    {
        base.Update();
    }
}
