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
        _leviathan.AttackCompo.Attack();
        FacingToPlayer();
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _leviathan.ChangeState(StateName.Idle);
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _leviathan.target.transform.position.x - _leviathan.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
