using UnityEngine;

public class Asmodeus : Enemy
{
    [SerializeField] private EntityFSMSO _asmodeusFSM;
    public AsmodeusAttackCompo AttackCompo;

    [field :SerializeField] public Transform DropPos;

    private EntityHealth _health;
    public EntityState CurrentState => _stateMachine.currentState;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();

        _health = GetCompo<EntityHealth>();
        _stateMachine = new StateMachine(_asmodeusFSM, this);
        AttackCompo = GetCompo<AsmodeusAttackCompo>();
        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;
        GetCompo<EntityAnimator>().OnPhase2Attack += Handle2Attack;
        GetCompo<EntityAnimator>().OnPhase3Attack += Handle3Attack;
        _health.OnHit += HandleHit;
        _health.OnDeath += HandleDead;
    }

    private void HandleDead()
    {
        _stateMachine.ChageState(StateName.Dead);
    }

    private void HandleHit(Entity dealer)
    {
        if (IsDead || isPhaseing) return;
        target = dealer as Player;
        ChangeState(StateName.Hit);
    }

    public void Handle2Attack()
    {
        AttackCompo.CharmAttack();
    }

    public void Handle3Attack()
    {
        AttackCompo.DarkAttack();
    }

    private void HandleAnimationEnd()
    {
        CurrentState.AnimationEndTrigger();
    }
    private void OnDestroy()
    {
        GetCompo<EntityAnimator>(true).OnAnimationEnd -= HandleAnimationEnd;
        GetCompo<EntityAnimator>().OnPhase2Attack -= Handle2Attack;
        GetCompo<EntityAnimator>().OnPhase3Attack -= Handle3Attack;
        _health.OnHit -= HandleHit;
        _health.OnDeath -= HandleDead;

    }

    private void Start()
    {
        _stateMachine.Initialize(StateName.Idle);
    }
}
