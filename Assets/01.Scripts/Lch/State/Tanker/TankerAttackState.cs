using UnityEngine;

public class TankerAttackState : EntityState
{

    private Tanker _tanker;
    private EntityMover _mover;
    private Vector2 _targetDir;

    public TankerAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _tanker = entity as Tanker;
        _renderer = _tanker.GetCompo<EntityRenderer>();
        _mover = _tanker.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(false);
        _targetDir = (_tanker.target.transform.position - _tanker.transform.position).normalized;
        _mover._moveSpeed = 18f;
        _mover.SetMovement(_targetDir.x);
        _tanker.AttackCompo.Attack();
    }

    public override void Update()
    {
        base.Update();
        FacingToPlayer();
        if (_tanker.CheckObstacleInFront())
        {
            _mover.StopImmediately(true);
            _tanker.ChangeState(StateName.Idle);
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _tanker.target.transform.position.x - _tanker.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
