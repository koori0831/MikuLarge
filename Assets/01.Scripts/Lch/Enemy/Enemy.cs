using UnityEngine;

public abstract class Enemy : Entity
{
    public Player target;
    [SerializeField] protected float _sightRange = 10f, _wallCheckRange = 1f;
    [SerializeField] protected LayerMask _whatIsPlayer, _whatIsObstacle;
    [SerializeField] private float _playercheckRadius = 10f , _attackRadius;
    [SerializeField] protected float _damge;
    [SerializeField] protected Vector2 _knockBackForce = new Vector2(5f, 3f);
    public Rigidbody2D RbCompo { get; protected set; }

    [field:SerializeField] public Coin DropCoin;
    [field: SerializeField] public DropItemListSO ItemList;

    protected override void Awake()
    {
        base.Awake();
        RbCompo = GetComponent<Rigidbody2D>();
    }

    [SerializeField] protected StateMachine _stateMachine;

    public void ChangeState(StateName newState)
    {
        _stateMachine.ChageState(newState);
    }

    public EntityState GetState(StateSO state)
    {
        return _stateMachine.GetState(state.stateName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                Vector2 atkDirection = gameObject.transform.right;
                Vector2 knockBackForce = _knockBackForce;
                knockBackForce.x *= atkDirection.x;
                damageable.ApplyDamage(_damge, atkDirection, knockBackForce, this);
            }
        }
    }

    protected virtual void Update()
    {
        _stateMachine.currentState.Update();
    }

    public bool CheckPlayerInRadius()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, _playercheckRadius, _whatIsPlayer);

        if (col != null && col.TryGetComponent(out Player player))
        {
            target = player;
            return true;
        }
        return false;
    }

    public bool CheckAttackToPlayerRadius()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, _attackRadius, _whatIsPlayer);
        if(col != null)
        {
            return true;
        }
        return false;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    public bool CheckObstacleInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _wallCheckRange, _whatIsObstacle);
        if (hit.transform != null)
        {
            return true;
        }
        return false;
    }

    protected virtual void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _playercheckRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * _sightRange);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * _wallCheckRange);
    }
}
