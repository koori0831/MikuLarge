using UnityEngine;
using DG.Tweening;
using System;

public class AsmodeusPhase1State : EntityState
{

    private Asmodeus _asmodeus;
    private SpriteRenderer _sprite;
    private BoxCollider2D _boxCollier;

    public AsmodeusPhase1State(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _asmodeus = entity as Asmodeus;
        _sprite = _asmodeus.GetComponentInChildren<SpriteRenderer>();
        _boxCollier = _asmodeus.GetComponent<BoxCollider2D>();
        
    }

    public override void Enter()
    {
        base.Enter();
        
        _asmodeus.AttackCompo.DashAttack();
        Sequence seq = DOTween.Sequence();
        seq.Append(_asmodeus.transform.DOMoveX(_asmodeus.target.transform.position.x, 1.5f)).AppendCallback(() => ChangeIdleState());
    }

    private void ChangeIdleState()
    {
        _sprite.enabled = true;
        _boxCollier.isTrigger = false;
        _asmodeus.ChangeState(StateName.Phase1End);
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _boxCollier.isTrigger = true;
            _sprite.enabled = false;
        }
        
    }
}
