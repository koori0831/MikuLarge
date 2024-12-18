using UnityEngine;

public class LeviathanPhase1State : EntityState
{

    private Leviathan _leviathan;

    public LeviathanPhase1State(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _leviathan = entity as Leviathan;
    }

    public override void Enter()
    {
        base.Enter();
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
