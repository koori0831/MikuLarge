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
        Vector2 targetDir = new Vector2(_asmodeus.target.transform.position.x - 2.5f, _asmodeus.target.transform.position.y);
        _mover.AddForceToEntity(targetDir);
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
