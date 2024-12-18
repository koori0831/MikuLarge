using UnityEngine;

public class AsmodeusPhase1State : EntityState
{

    private Asmodeus _asmodeus;
    private EntityMover _mover;

    public AsmodeusPhase1State(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _asmodeus = entity as Asmodeus;
        _mover = _asmodeus.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(false);
        _mover.CanManualMove = false;
        _asmodeus.AttackCompo.DarkAttack();
    }

    public override void Update()
    {
        base.Update();
        if(_asmodeus.RbCompo.linearVelocity.x <= 0)
        {
            _asmodeus.ChangeState(StateName.Idle);
        }
    }
}
