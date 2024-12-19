using System;
using UnityEngine;

public class ADEnemy : Enemy
{
    [SerializeField] private EntityFSMSO _adEnemyFsm;
    private EntityHealth _health;
    public ADEnemyAttackCompo AttackCompo;
    public EntityState CurrentState => _stateMachine.currentState;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _health = GetCompo<EntityHealth>();
        AttackCompo = GetCompo<ADEnemyAttackCompo>();
        _stateMachine = new StateMachine(_adEnemyFsm, this);
        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;
        GetCompo<EntityAnimator>().OnAttackEvent += HandleAttack;
        _health.OnHit += HandleHit;
        _health.OnDeath += HandleDead;
    }

    private void HandleAttack()
    {
        AttackCompo.Attack();
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
        GetCompo<EntityAnimator>().OnAttackEvent -= HandleAttack;
        GetCompo<EntityAnimator>(true).OnAnimationEnd -= HandleAnimationEnd;
        _health.OnHit -= HandleHit;
        _health.OnDeath -= HandleDead;
    }

    private void Start()
    {
        _stateMachine.Initialize(StateName.Idle);
    }
}
