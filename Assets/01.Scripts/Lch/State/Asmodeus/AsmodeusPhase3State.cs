using UnityEngine;

public class AsmodeusPhase3State : EntityState
{

    private Asmodeus _asmodeus;

    public AsmodeusPhase3State(Entity entity, AnimParamSO animParam) : base(entity, animParam)
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
