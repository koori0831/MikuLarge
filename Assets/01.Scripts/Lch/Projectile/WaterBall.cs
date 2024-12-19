using UnityEngine;

public class WaterBall : Entity
{
    private Transform _target;
    [SerializeField] private float _shotSpeed = 5;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(5f, 3f);
    [SerializeField] private float _damge;
    private Rigidbody2D _rbCompo;
    //[SerializeField] private Animator _animator;
    //[SerializeField] private AnimParamSO _triggerParam;
    private float _lifeTime = 3f; private Leviathan _leviathan;
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
    }


    private void Update()
    {

        FacingToPlayer();

        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                Vector2 atkDirection = gameObject.transform.right;
                Vector2 knockBackForce = _knockBackForce;
                knockBackForce.x *= atkDirection.x;
                damageable.ApplyDamage(_damge, atkDirection, -knockBackForce, this);
            }
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    private void FacingToPlayer()
    {
        float xDirection = _leviathan.target.transform.position.x - transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
