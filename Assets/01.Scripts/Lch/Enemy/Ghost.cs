using UnityEngine;

public class Ghost : Enemy
{
    [SerializeField] private EntityFSMSO _ghostFSM;
    private DamageCast _damgeCast;
    public GhostAttackCompo AttackCompo;

    public EntityState CurrentState => _stateMachine.currentState;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _stateMachine = new StateMachine(_ghostFSM, this);
        _damgeCast = GetCompo<DamageCast>();
        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;
        GetCompo<EntityAnimator>().OnAttackEvent += Attack;
        _damgeCast.InitCaster(this);
        AttackCompo = GetCompo<GhostAttackCompo>();
    }

    public void Attack()
    {
        _damgeCast.CastDamage();
    }

    private void HandleAnimationEnd()
    {
        CurrentState.AnimationEndTrigger();
    }
    private void OnDestroy()
    {
        GetCompo<EntityAnimator>(true).OnAnimationEnd -= HandleAnimationEnd;
        GetCompo<EntityAnimator>().OnAttackEvent -= Attack;

    }

    private void Start()
    {
        _stateMachine.Initialize(StateName.Idle);
    }
}
