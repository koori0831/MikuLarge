using UnityEngine;

public class WaterArrow : Entity
{
    private Transform _target;
    [SerializeField] private float _shotSpeed = 5;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(5f, 3f);
    [SerializeField] private float _damge;
    private Rigidbody2D _rbCompo;
    private float _lifeTime = 3;
    private Leviathan _leviathan;
    private EntityRenderer _renderer;

    protected override void Awake()
    {
        base.Awake();
        _target = GameObject.FindWithTag("Player").transform;
        _rbCompo = GetComponent<Rigidbody2D>();
        _leviathan = GameObject.FindWithTag("Enemy").GetComponent<Leviathan>();
        _renderer = _leviathan.GetCompo<EntityRenderer>();
    }

    private void Start()
    {
        Vector2 targetDir = _target.position - transform.position;
        _rbCompo.linearVelocity = targetDir.normalized * _shotSpeed;
        FacingToPlayer();
    }
    private void Update()
    {


        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                Vector2 atkDirection = transform.right;
                Vector2 knockBackForce = _knockBackForce;
                knockBackForce.x *= atkDirection.x;
                damageable.ApplyDamage(_damge, atkDirection, -knockBackForce, this);
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

    }

    private void FacingToPlayer()
    {
        float xDirection = _leviathan.target.transform.position.x - transform.position.x;
        if (Mathf.Abs(xDirection) < 0.5f)
        {
            transform.Rotate(0, 180f, 0);
        }
    }
}
