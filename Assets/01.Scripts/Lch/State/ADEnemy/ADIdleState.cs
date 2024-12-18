using UnityEngine;

public class ADIdleState : EntityState
{

    private ADEnemy _adEnemy;
    private EntityMover _mover;


    public ADIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _adEnemy = entity as ADEnemy;
        _mover = _adEnemy.GetCompo<EntityMover>();
        
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
    }

    public override void Update()
    {
        base.Update();
        
        if (_adEnemy.CheckAttackToPlayerRadius() && _adEnemy.AttackCompo.CanAttack() && _adEnemy.CheckPlayerInRadius())
        {
            _adEnemy.ChangeState(StateName.Attack);
        }
    }
}
