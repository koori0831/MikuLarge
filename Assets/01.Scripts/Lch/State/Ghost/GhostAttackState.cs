using UnityEngine;

public class GhostAttackState : EntityState
{
    private Ghost _ghost;
    public GhostAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _ghost = entity as Ghost;
    }

    public override void Enter()
    {
        base.Enter();
        _ghost.AttackCompo.Attack();
    }

    public override void Update()
    {
        base.Update();
        FacingToPlayer();
        if (_isTriggerCall)
        {
            _ghost.ChangeState(StateName.Move);
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _ghost.target.transform.position.x - _ghost.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
