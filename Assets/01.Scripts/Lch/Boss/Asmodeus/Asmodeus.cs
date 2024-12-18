using UnityEngine;

public class Asmodeus : Enemy
{
    [SerializeField] private EntityFSMSO _asmodeusFSM;
    public AsmodeusAttackCompo AttackCompo;

    private EntityHealth _health;
    public EntityState CurrentState => _stateMachine.currentState;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();

        _health = GetCompo<EntityHealth>();
        _stateMachine = new StateMachine(_asmodeusFSM, this);
        AttackCompo = GetCompo<AsmodeusAttackCompo>();
        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;
        GetCompo<EntityAnimator>().OnAttackEvent += HandleAttack;
        _health.OnHit += HandleHit;
        _health.OnDeath += HandleDead;
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
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

    public void HandleAttack()
    {
        
    }

    private void HandleAnimationEnd()
    {
        CurrentState.AnimationEndTrigger();
    }
    private void OnDestroy()
    {
        GetCompo<EntityAnimator>(true).OnAnimationEnd -= HandleAnimationEnd;
        GetCompo<EntityAnimator>().OnAttackEvent -= HandleAttack;
        _health.OnHit -= HandleHit;
        _health.OnDeath -= HandleDead;

    }

    private void Start()
    {
        _stateMachine.Initialize(StateName.Idle);
    }
}
