using UnityEngine;
using Ami.BroAudio;

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
        BroAudio.Play(_asmodeus.Phase2Sound);
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
