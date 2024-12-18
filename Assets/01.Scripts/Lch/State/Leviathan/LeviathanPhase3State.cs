using UnityEngine;

public class LeviathanPhase3State : EntityState
{

    private Leviathan _leviathan;
    private EntityMover _mover;

    public LeviathanPhase3State(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _leviathan = entity as Leviathan;
        _mover = _leviathan.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
        _leviathan.AttackCompo.WaterBallAttack();
    }

    public override void Update()
    {
        base.Update();
    }
}
