using System;
using UnityEngine;
using System.Collections;

public class TankerAttackState : EntityState
{

    private Tanker _tanker;
    private EntityMover _mover;
    private Vector2 _targetDir;
    private float _attackStart;
    private float _attackStopTime = 2.5f;

    public TankerAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _tanker = entity as Tanker;
        _renderer = _tanker.GetCompo<EntityRenderer>();
        _mover = _tanker.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(false);
        _tanker.AttackCompo.Attack();
    }

    public override void Update()
    {
        base.Update();
        FacingToPlayer();
        _tanker.StartCoroutine(CastDealy());
        _targetDir = (_tanker.target.transform.position - _tanker.transform.position).normalized;
        _mover._moveSpeed = 9f;
        _mover.SetMovement(_targetDir.x);
        _attackStart += Time.deltaTime;

        if(_attackStart >= _attackStopTime)
        {
            _mover.StopImmediately();
            _tanker.ChangeState(StateName.Idle);
        }
    }

    private IEnumerator CastDealy()
    {
        yield return new WaitForSeconds(0.2F);
        _tanker.Caster.CastDamage();
    }

    private void FacingToPlayer()
    {
        float xDirection = _tanker.target.transform.position.x - _tanker.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
