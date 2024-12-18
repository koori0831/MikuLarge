using System;
using UnityEngine;

public class Tanker : Enemy
{
    [SerializeField] private EntityFSMSO _tankerFsm;
    private EntityHealth _health;
    public EnemyAttackCompo AttackCompo;
    public EntityState CurrentState => _stateMachine.currentState;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _health = GetCompo<EntityHealth>();
        AttackCompo = GetCompo<EnemyAttackCompo>();
        _stateMachine = new StateMachine(_tankerFsm, this);
        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;
        _health.OnHit += HandleHit;
        _health.OnDeath += HandleDead;
    }

    private void HandleDead()
    {
        _stateMachine.ChageState(StateName.Dead);
    }

    private void HandleHit(Entity dealer)
    {
        if (IsDead) return;
        target = dealer as Player;
        ChangeState(StateName.Hit);
    }

    private void HandleAnimationEnd()
    {
        CurrentState.AnimationEndTrigger();
    }
    private void OnDestroy()
    {
        GetCompo<EntityAnimator>(true).OnAnimationEnd -= HandleAnimationEnd;
        _health.OnHit -= HandleHit;
        _health.OnDeath -= HandleDead;
    }

    private void Start()
    {
        _stateMachine.Initialize(StateName.Idle);
    }
}
