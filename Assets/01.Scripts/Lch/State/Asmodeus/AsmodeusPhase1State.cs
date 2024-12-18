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
        _asmodeus.AttackCompo.DashAttack();
        Vector2 targetDir = _asmodeus.target.transform.position - _asmodeus.transform.position;
        _mover.AddForceToEntity(targetDir * 4f);
    }

    public override void Update()
    {
        base.Update();
        if (_asmodeus.CheckObstacleInFront())
        {
            _asmodeus.ChangeState(StateName.Idle);
        }
    }
}
