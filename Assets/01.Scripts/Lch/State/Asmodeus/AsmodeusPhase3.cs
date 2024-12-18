using UnityEngine;

public class AsmodeusPhase3 : EntityState
{

    private Asmodeus _asmodeus;

    public AsmodeusPhase3(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _asmodeus = entity as Asmodeus;
    }

    public override void Enter()
    {
        base.Enter();
        _asmodeus.AttackCompo.DarkAttack();
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
