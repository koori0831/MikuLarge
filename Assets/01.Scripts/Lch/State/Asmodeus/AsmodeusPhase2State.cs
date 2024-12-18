using UnityEngine;

public class AsmodeusPhase2State : EntityState
{

    private Asmodeus _asmodeus;

    public AsmodeusPhase2State(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _asmodeus = entity as Asmodeus;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _asmodeus.ChangeState(StateName.Idle);
        }
    }
}
