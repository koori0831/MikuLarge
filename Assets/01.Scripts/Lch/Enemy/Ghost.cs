using UnityEngine;

public class Ghost : Enemy
{
    [SerializeField] private EntityFSMSO _ghostFSM;
    private DamageCast damgeCast;

    public EntityState CurrentState => _stateMachine.currentState;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _stateMachine = new StateMachine(_ghostFSM, this);
        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;
        damgeCast = GetCompo<DamageCast>();
    }

    private void HandleAnimationEnd()
    {
        CurrentState.AnimationEndTrigger();
    }
    private void OnDestroy()
    {
        GetCompo<EntityAnimator>(true).OnAnimationEnd -= HandleAnimationEnd;
    }

    private void Start()
    {
        _stateMachine.Initialize(StateName.Idle);
    }
}
