using Ami.BroAudio;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : Entity
{
    [Header("FSM")]
    [SerializeField] private EntityFSMSO _playerFSM;

    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
    public float jumpPower = 12f;
    public int jumpCount = 2;
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    [SerializeField] private float _interactRange = 3f;
    [SerializeField] private LayerMask _interatable;
    public Hands Hands;
    public LayerMask dashExclude;

    public bool charmed;

    public bool isReloading;

    public bool isHit;

    private int _currentJumpCount = 0;
    private EntityMover _mover;
    private PlayerAttackCompo _atkCompo;
    private EntityHealth _health;

    [SerializeField] private StateMachine _stateMachine;

    public EntityState CurrentState => _stateMachine.currentState;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _stateMachine = new StateMachine(_playerFSM, this);

        _mover = GetCompo<EntityMover>();
        Hands = GetCompo<Hands>();
        _health = GetCompo<EntityHealth>();
        _mover.OnGroundStatusChange += HandleGroundStatusChange;
        PlayerInput.JumpEvent += HandleJumpEvent;
        PlayerInput.InteractEvent += HadleInteractEvent;

        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;

        _atkCompo = GetCompo<PlayerAttackCompo>();
        PlayerInput.MeleeEvent += HandleAttackKeyEvent;
        PlayerInput.DashEvent += HandleDashEvent;
        PlayerInput.ShotEvent += HandleShotEvent;
        _health.OnHit += HandleHit;
        _health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        _stateMachine.ChageState(StateName.Dead);
    }

    private void HandleHit(Entity entity)
    {
        if (IsDead) return;
        ChangeState(StateName.Hit);
    }

    private void OnDestroy()
    {
        _mover.OnGroundStatusChange -= HandleGroundStatusChange;
        PlayerInput.JumpEvent -= HandleJumpEvent;
        GetCompo<EntityAnimator>(true).OnAnimationEnd -= HandleAnimationEnd;
        PlayerInput.MeleeEvent -= HandleAttackKeyEvent;
        PlayerInput.DashEvent -= HandleDashEvent;
        PlayerInput.InteractEvent -= HadleInteractEvent;
        PlayerInput.ShotEvent -= HandleShotEvent;
    }

    protected void Start()
    {
        _stateMachine.Initialize(StateName.Idle);
    }

    private void Update()
    {
        _stateMachine.UpdateStateMachine();
        if (charmed)
        {
            StartCoroutine(RollBackPlayer());
        }

    }

    private IEnumerator RollBackPlayer()
    {
        yield return new WaitForSeconds(3f);
        PlayerInput.Controls.Player.Enable();
    }

    public void ChangeState(StateName newState)
    {
        _stateMachine.ChageState(newState);
    }

    public EntityState GetState(StateSO state)
    {
        return _stateMachine.GetState(state.stateName);
    }

    private void HadleInteractEvent()
    {
        Collider2D obj = Physics2D.OverlapCircle(transform.position, _interactRange, _interatable);
        if (obj != null && !isReloading && !isHit)
        {
            if (obj.TryGetComponent(out IInteractable target))
            {
                target.Interact(this);
            }
        }
    }

    private void HandleJumpEvent()
    {
        if (_mover.IsGrounded || _currentJumpCount > 0 && !isHit)
        {
            _currentJumpCount--;
            ChangeState(StateName.Jump);
        }
    }

    private void HandleGroundStatusChange(bool isGrounded)
    {
        if (isGrounded)
            _currentJumpCount = jumpCount;
    }

    private void HandleAnimationEnd()
    {
        _stateMachine.currentState.AnimationEndTrigger();
        _stateMachine.ChageState(StateName.Idle);
    }

    private void HandleAttackKeyEvent()
    {
        if (_atkCompo.AttemptAttack() && !isReloading&& !isHit)
        {
            ChangeState(StateName.Melee);
        }
    }

    private void HandleDashEvent()
    {
        if (_atkCompo.AttemptDash() && !isHit)
        {
            ChangeState(StateName.Dash);
        }
    }

    private void HandleShotEvent()
    {
        if (_atkCompo.AttemptShot() && !isReloading && !isHit)
        {
            switch (Hands.nowWeapon)
            {
                case WeaponType.handGun:Hands.currentHandGun.Shot();  break;
                case WeaponType.handsGun: Hands.currentHandGun.Shot(); break;
                case WeaponType.melee: HandleAttackKeyEvent(); break;
            }
        }
    }
    public void HidingGun(bool value)
    {
        switch (Hands.nowWeapon)
        {
            case WeaponType.handGun:
                Hands.currentHandGun.gameObject.GetComponent<SpriteRenderer>().enabled = value; break;
            case WeaponType.handsGun:
                Hands.currentHandsGun.gameObject.GetComponent<SpriteRenderer>().enabled = value; break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _interactRange);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
        }
    }
}
