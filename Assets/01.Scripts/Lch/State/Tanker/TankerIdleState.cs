using UnityEngine;

public class TankerIdleState : EntityState
{

    private Tanker _tanker;
    private EntityMover _mover;

    public TankerIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _tanker = entity as Tanker;
        _mover = _tanker.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
    }

    public override void Update()
    {
        base.Update();
        if (_tanker.CheckPlayerInRadius() && _tanker.AttackCompo.CanAttack())
        {
            _tanker.ChangeState(StateName.Attack);
        }
    }
}
