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
        FacingToPlayer();
        if (_isTriggerCall)
        {
            _asmodeus.ChangeState(StateName.Idle);
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _asmodeus.target.transform.position.x - _asmodeus.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
