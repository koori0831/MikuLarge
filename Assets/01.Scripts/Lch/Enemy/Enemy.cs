using UnityEngine;

public abstract class Enemy : Entity
{
    public Player target;
    [SerializeField] protected float _sightRange = 10f, _wallCheckRange = 1f;
    [SerializeField] protected LayerMask _whatIsPlayer, _whatIsObstacle;
    [SerializeField] private float _playercheckRadius = 10f , _attackRadius;
    public Rigidbody2D RbCompo { get; protected set; }

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


    //public RaycastHit2D CheckObstacleInFront()
    //{
    //    return Physics2D.Raycast(transform.position, transform.right, _wallCheckRange, _whatIsObstacle);
    //}

    protected virtual void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _playercheckRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * _sightRange);

        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position, transform.position + transform.right * _wallCheckRange);
    }
}
