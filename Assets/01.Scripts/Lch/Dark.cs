using System;
using System.Collections;
using UnityEngine;

public class Dark : Entity
{
    private Transform _target;
    [SerializeField] private float _shotSpeed = 5;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(5f, 3f);
    [SerializeField] private float _damge;
    private Rigidbody2D _rbCompo;
    private Asmodeus _asmodeus;
    private Vector2 _targetDir;
    private float _lifeTime = 3;

    protected override void Awake()
    {
        base.Awake();
        _target = GameObject.FindWithTag("Player").transform;
        _rbCompo = GetComponent<Rigidbody2D>();
        _asmodeus = GameObject.FindWithTag("Enemy").GetComponent<Asmodeus>();
    }

    private void Update()
    {
        _targetDir = _target.position - transform.position;
        _rbCompo.linearVelocity = _targetDir.normalized * _shotSpeed;
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                Vector2 atkDirection = gameObject.transform.right;
                Vector2 knockBackForce = _knockBackForce;
                knockBackForce.x *= atkDirection.x;
                damageable.ApplyDamage(_damge, atkDirection, -knockBackForce, this);
            }
            Destroy(gameObject);
        }
    }
}
