using System.Collections;
using UnityEngine;

public class Charm : Entity
{
    private Transform _target;
    [SerializeField] private float _shotSpeed = 5;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(5f, 3f);
    [SerializeField] private float _damge;
    private Rigidbody2D _rbCompo;
    private EntityMover _mover;
    private Asmodeus _asmodeus;

    protected override void Awake()
    {
        base.Awake();
        _target = GameObject.Find("Player").transform;
        _rbCompo = GetComponent<Rigidbody2D>();
        _asmodeus = GameObject.Find("Enemy").GetComponent<Asmodeus>();
    }

    private void Start()
    {
        Vector2 targetDir = _target.position - transform.position;
        _rbCompo.linearVelocity = targetDir.normalized * _shotSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            if(collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                Vector2 atkDirection = gameObject.transform.right;
                Vector2 knockBackForce = _knockBackForce;
                knockBackForce.x *= atkDirection.x;
                damageable.ApplyDamage(_damge, atkDirection, -knockBackForce, this);
            }
          
            StartCoroutine(PlayerAttack(player));
        }
    }

    private IEnumerator PlayerAttack(Player player)
    {
        player.PlayerInput.Controls.Disable();
        _mover = player.GetCompo<EntityMover>();
        _mover.AddForceToEntity(_asmodeus.transform.position);
        yield return new WaitForSeconds(3f);
    }
}
