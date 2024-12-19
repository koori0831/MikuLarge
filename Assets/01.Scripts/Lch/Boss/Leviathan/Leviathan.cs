using UnityEngine;

public class Leviathan : Enemy
{
    [SerializeField] private EntityFSMSO _asmodeusFSM;
    public LeviathanAttackCompo AttackCompo;

    private EntityHealth _health;
    public EntityState CurrentState => _stateMachine.currentState;

    public DamageCast _damgeCast;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _damgeCast = GetComponentInChildren<DamageCast>();
        _health = GetCompo<EntityHealth>();
        _stateMachine = new StateMachine(_asmodeusFSM, this);
        AttackCompo = GetCompo<LeviathanAttackCompo>();
        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;
        GetCompo<EntityAnimator>().OnAttackEvent += HandleAttack;
        GetCompo<EntityAnimator>().OnPhase2Attack += AttackCompo.WaterArrowAttack;
        GetCompo<EntityAnimator>().OnPhase3Attack += AttackCompo.WaterBallAttack;
        _damgeCast.InitCaster(this);
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

    public void HandleAttack()
    {
        _damgeCast.CastDamage();
    }

    private void HandleAnimationEnd()
    {
        CurrentState.AnimationEndTrigger();
    }
    private void OnDestroy()
    {
        GetCompo<EntityAnimator>().OnAttackEvent -= HandleAttack;
        GetCompo<EntityAnimator>().OnPhase2Attack -= AttackCompo.WaterArrowAttack;
        GetCompo<EntityAnimator>().OnPhase3Attack -= AttackCompo.WaterBallAttack;
        _health.OnHit -= HandleHit;
        _health.OnDeath -= HandleDead;

    }

    private void Start()
    {
        _stateMachine.Initialize(StateName.Idle);
    }
}
