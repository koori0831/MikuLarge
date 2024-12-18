using UnityEngine;

public class TankerAttackState : EntityState
{

    private Tanker _tanker;
    private EntityMover _mover;
    private Vector2 _targetDir;
    private EntityRenderer _renderer;

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
    }

    public override void Update()
    {
        base.Update();
        _renderer.FlipController(_targetDir.x);
    }
}
