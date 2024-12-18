using UnityEngine;
using System;

public class Bullet : Entity
{
    [SerializeField] private Rigidbody2D _rbCompo;
    [SerializeField] private float _damge;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(5f, 3f);
    //[SerializeField] private Animator _animator;
    //[SerializeField] private AnimParamSO _triggerParam;
    private float _lifeTime;
    private bool _canExplosion;

    public void ThrowBullet(Vector2 velocity, float lifeTime)
    {
        _canExplosion = true;
        _lifeTime = lifeTime;
        _rbCompo.AddForce(velocity, ForceMode2D.Impulse);
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0 && _canExplosion)
        {
            _canExplosion = false;
            //TriggerExplosion();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (TryGetComponent(out IDamageable damageable))
            {
                Vector2 atkDirection = gameObject.transform.right;
                Vector2 knockBackForce = _knockBackForce;
                knockBackForce.x *= atkDirection.x;
                damageable.ApplyDamage(_damge, atkDirection, knockBackForce, this);
            }
                Destroy(gameObject);
        }
    }

    //private void TriggerExplosion()
    //{
    //    _animator.SetTrigger(_triggerParam.hashValue);
    //}

}
