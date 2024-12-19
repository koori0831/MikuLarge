using UnityEngine;
using System;

public class Bullet : Entity
{
    private Transform _target;
    [SerializeField] private Rigidbody2D _rbCompo;
    [SerializeField] private float _damge;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(5f, 3f);
    //[SerializeField] private Animator _animator;
    //[SerializeField] private AnimParamSO _triggerParam;
    private float _lifeTime;
    private bool _canExplosion;
    private EntityRenderer _renderer;
    private ADEnemy _adEnemy;

    protected override void Awake()
    {
        base.Awake();
        _target = GameObject.FindWithTag("Player").transform;
        _rbCompo = GetComponent<Rigidbody2D>();
        _adEnemy = GameObject.FindWithTag("ADEnemy").GetComponent<ADEnemy>();
        _renderer = _adEnemy.GetCompo<EntityRenderer>();
    }

    public void ThrowBullet(Vector2 velocity, float lifeTime)
    {
        _canExplosion = true;
        _lifeTime = lifeTime;
        _rbCompo.AddForce(velocity,ForceMode2D.Impulse);
    }

    private void Update()
    {
        FacingToPlayer();
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0 && _canExplosion)
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
        float xDirection = _adEnemy.target.transform.position.x - transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }

    //private void TriggerExplosion()
    //{
    //    _animator.SetTrigger(_triggerParam.hashValue);
    //}

}
