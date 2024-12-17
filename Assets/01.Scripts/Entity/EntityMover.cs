using System;
using UnityEngine;

public class EntityMover : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float _moveSpeed = 5f;
    
    [SerializeField] private Transform _groundTrm;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector2 _groundCheckSize;

    [SerializeField] private AnimParamSO _ySpeedParam;

    public bool CanManualMove { get; set; } = true;
    public Vector2 Velocity => _rbCompo.linearVelocity;
    public bool IsGrounded { get; private set; }
    public event Action<bool> OnGroundStatusChange;
    public event Action<Vector2> OnMoveVelocity;

    private Entity _entity;
    private EntityRenderer _renderer;
    private Rigidbody2D _rbCompo;
    private float _movementX;
    
    private float _moveSpeedMultiplier, _originalGravityScale;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
        _renderer = _entity.GetCompo<EntityRenderer>();
        _rbCompo = entity.GetComponent<Rigidbody2D>();
        
        _originalGravityScale = _rbCompo.gravityScale;
        _moveSpeedMultiplier = 1f;
    }
    
    public void SetMoveSpeedMultiplier(float value) => _moveSpeedMultiplier = value;
    public void SetGravityScale(float value) => _rbCompo.gravityScale = _originalGravityScale * value;

    public void AddForceToEntity(Vector2 force)
    {
        _rbCompo.AddForce(force, ForceMode2D.Impulse);
    }

    public void StopImmediately(bool isYAxisToo = false)
    {
        if (isYAxisToo)
            _rbCompo.linearVelocity = Vector2.zero;
        else
            _rbCompo.linearVelocityX = 0;
        _movementX = 0;
    }
    
    public void SetMovement(float x)
    {
        _movementX = x;
    }
    
    private void FixedUpdate()
    {
        CheckGround();
        MoveCharacter();
    }
    
    private void CheckGround()
    {
        bool before = IsGrounded;
        IsGrounded = Physics2D.OverlapBox(_groundTrm.position, _groundCheckSize, 0, _groundMask);
        if (before != IsGrounded)
            OnGroundStatusChange?.Invoke(IsGrounded);
    }

    private void MoveCharacter()
    {
        if (CanManualMove)
        {
            _renderer.FlipController(_movementX);
            _rbCompo.linearVelocityX = _movementX * _moveSpeed * _moveSpeedMultiplier;
        }
        
        _renderer.SetParam(_ySpeedParam, _rbCompo.linearVelocity.y);
        OnMoveVelocity?.Invoke(_rbCompo.linearVelocity);
    }
    
    private void OnDrawGizmos()
    {
        if (_groundTrm == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundTrm.position, _groundCheckSize);
    }
}
