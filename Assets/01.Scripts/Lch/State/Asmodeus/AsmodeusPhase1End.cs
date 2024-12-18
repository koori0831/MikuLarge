using UnityEngine;

public class AsmodeusPhase1End : EntityState
{

    private Asmodeus _asmodeus;

    public AsmodeusPhase1End(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _asmodeus = entity as Asmodeus;
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
