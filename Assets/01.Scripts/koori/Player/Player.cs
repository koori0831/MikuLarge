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
    [field :SerializeField] public PlayerSavesSO PlayerSave;
    [SerializeField] private PlayerSaveSoEventChannelSO _soEvent;
    [SerializeField] private SoundID melee;

    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
    public float jumpPower = 12f;
    public int jumpCount = 2;
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    [SerializeField] private float _interactRange = 3f;
    [SerializeField] private LayerMask _interatable;
    public Hands Hands;
    public GameObject ReLoadOb;
    public LayerMask dashExclude;

    public bool charmed;

    public bool isReloading;

    public Transform nailPos;

    public GameObject Chain;

    public BoolEventChannelSO  NailEvent;

    public bool isHit;

    public bool isNailed;

    private int _currentJumpCount = 0;
    private EntityMover _mover;
    private PlayerAttackCompo _atkCompo;
    public EntityHealth health;

    [SerializeField] private StateMachine _stateMachine;

    public EntityState CurrentState => _stateMachine.currentState;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _stateMachine = new StateMachine(_playerFSM, this);

        _mover = GetCompo<EntityMover>();
        Hands = GetCompo<Hands>();
        health = GetCompo<EntityHealth>();
        _mover.OnGroundStatusChange += HandleGroundStatusChange;
        PlayerInput.JumpEvent += HandleJumpEvent;
        PlayerInput.InteractEvent += HadleInteractEvent;
        PlayerInput.NailEvent += HandleNailEvent;

        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;
        GetCompo<EntityAnimator>(true).OnNailShot += Nail;

        _atkCompo = GetCompo<PlayerAttackCompo>();
        PlayerInput.MeleeEvent += HandleAttackKeyEvent;
        PlayerInput.DashEvent += HandleDashEvent;
        PlayerInput.ShotEvent += HandleShotEvent;
        health.OnHit += HandleHit;
        health.OnDeath += HandleDeath;

        ReLoadOb.SetActive(false);
        _mover.IsPlayer();

        NailEvent.OnValueEvent += RevertInput;
        PlayerSave.CurrentHealth = health._currentHealth;
        PlayerSave.nowWeaponType = Hands.nowWeapon;
        PlayerSave.currentHandGun = Hands.currentHandGun;
        PlayerSave.currentHandsGun = Hands.currentHandsGun;

        if (GameManger.Instance.SaveSo != null)
        {
            health._currentHealth = GameManger.Instance.SaveSo.CurrentHealth;
            Hands.nowWeapon = GameManger.Instance.SaveSo.nowWeaponType;
            Hands.currentHandGun = GameManger.Instance.SaveSo.currentHandGun;
            Hands.currentHandsGun = GameManger.Instance.SaveSo.currentHandsGun;

            Hands.WeaponChange();
        }
        //PlayerSave.NowCoin = Manager.manager.ResourceManager.Coin;
    }

    private void RevertInput(bool obj)
    {
        PlayerInput.Controls.Enable();
        health._currentHealth = 20;

        isNailed = obj;
    }

    private void HandleNailEvent()
    {
        if (!Manager.manager.ResourceManager.CanNeailUse)
            return;
        ChangeState(StateName.Nail);  
    }

    private void HandleDeath()
    {
        GameManger.Instance.SaveSo = null;
        ChangeState(StateName.Dead);
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
        GetCompo<EntityAnimator>(true).OnNailShot -= Nail;
        PlayerInput.MeleeEvent -= HandleAttackKeyEvent;
        PlayerInput.DashEvent -= HandleDashEvent;
        PlayerInput.InteractEvent -= HadleInteractEvent;
        PlayerInput.ShotEvent -= HandleShotEvent;
        PlayerInput.NailEvent -= HandleNailEvent;
        health.OnHit -= HandleHit;
        health.OnDeath -= HandleDeath;
        PlayerSave.CurrentHealth = health._currentHealth;
        PlayerSave.nowWeaponType = Hands.nowWeapon;
        PlayerSave.currentHandGun = Hands.currentHandGun;
        PlayerSave.currentHandsGun = Hands.currentHandsGun;
        PlayerSave.NowCoin = Manager.manager.ResourceManager.Coin;
        _soEvent.RaiseEvent(PlayerSave);
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
        if (_mover.IsGrounded || _currentJumpCount > 0 && !isHit && !IsDead)
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
    }

    private void HandleAttackKeyEvent()
    {
        if (_atkCompo.AttemptAttack() && !isReloading&& !isHit && !IsDead)
        {
            BroAudio.Play(melee);
            ChangeState(StateName.Melee);
        }
    }

    private void HandleDashEvent()
    {
        if (_atkCompo.AttemptDash() && !isHit && !IsDead)
        {
            ChangeState(StateName.Dash);
        }
    }

    private void HandleShotEvent()
    {
        if (_atkCompo.AttemptShot() && !isReloading && !isHit && !IsDead)
        {
            switch (Hands.nowWeapon)
            {
                case WeaponType.handGun:Hands.currentHandGun.Shot();  break;
                case WeaponType.handsGun: Hands.currentHandsGun.Shot(); break;
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
    private void Nail()
    {
        Instantiate(Chain, nailPos.position, Quaternion.identity);
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
