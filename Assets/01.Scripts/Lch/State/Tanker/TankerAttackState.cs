using System;
using UnityEngine;
using System.Collections;

public class TankerAttackState : EntityState
{

    private Tanker _tanker;
    private EntityMover _mover;
    private Vector2 _targetDir;
    private float _attackStart;
    private float _attackStopTime = 1f;
    private float _attackCount = 0f;

    public TankerAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _tanker = entity as Tanker;
        _renderer = _tanker.GetCompo<EntityRenderer>();
        _mover = _tanker.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _targetDir = (_tanker.target.transform.position - _tanker.transform.position).normalized;
        _mover._moveSpeed = 9f;
        _mover.SetMovement(_targetDir.x);
        _attackCount = 0;
        _tanker.AttackCompo.Attack();
    }

    public override void Update()
    {
        base.Update();
        FacingToPlayer();
        if(_tanker.Caster.CastDamage() && _attackCount == 0)
        {
            _tanker.ChangeState(StateName.Idle);
            _attackCount++;
        }

        _attackStart += Time.deltaTime;

        if(_attackStart >= _attackStopTime)
        {
            _mover.StopImmediately();
            _tanker.ChangeState(StateName.Idle);
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _tanker.target.transform.position.x - _tanker.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
