using UnityEngine;

public class GhostChaseState : EntityState
{
    private Ghost _ghost;
    private EntityMover _mover;
    public GhostChaseState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _ghost = entity as Ghost;
       _mover = _ghost.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(false);
    }

    public override void Update()
    {
        base.Update();
        Vector2 targetDir = _ghost.target.transform.position - _ghost.transform.position;

        _mover.SetMovement(targetDir.normalized.x);

        if (_ghost.CheckAttackToPlayerRadius()&& _ghost.AttackCompo.CanAttack())
        {
            _ghost.ChangeState(StateName.Attack);
        }
    }
}
