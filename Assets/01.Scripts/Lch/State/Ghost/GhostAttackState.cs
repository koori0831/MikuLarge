using UnityEngine;

public class GhostAttackState : EntityState
{
    private Ghost _ghost;
    public GhostAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _ghost = entity as Ghost;
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _ghost.ChangeState(StateName.Idle);
        }
    }
}
