using UnityEngine;

public class AsmodeusDeadState : EntityState
{
    private Asmodeus _asmodeus;
    public AsmodeusDeadState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _asmodeus = entity as Asmodeus;
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _asmodeus.BossDeadEvnet.RaiseEvent(true);
            GameObject.Destroy(_asmodeus.gameObject);
        }
    }
}
