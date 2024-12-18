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

    protected override void Awake()
    {
        base.Awake();
        _target = GameObject.Find("Player").transform;
        _rbCompo = GetComponent<Rigidbody2D>();
        _asmodeus = GameObject.Find("Enemy").GetComponent<Asmodeus>();
    }

    private void Start()
    {
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }

    private void Update()
    {
        _targetDir = _target.position - transform.position;
        _rbCompo.linearVelocity = _targetDir.normalized * _shotSpeed;
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
        }
    }
}
