using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Charm : Entity
{
    private Transform _target;
    [SerializeField] private float _shotSpeed = 5;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(5f, 3f);
    [SerializeField] private float _damge;
    private Rigidbody2D _rbCompo;
    private EntityMover _mover;
    private Asmodeus _asmodeus;
    private float _lifeTime = 3;

    protected override void Awake()
    {
        base.Awake();
        _target = GameObject.FindWithTag("Player").transform;
        _rbCompo = GetComponent<Rigidbody2D>();
        _asmodeus = GameObject.FindWithTag("Enemy").GetComponent<Asmodeus>();
    }

    private void Start()
    {
        Vector2 targetDir = _target.position - transform.position;
        _rbCompo.linearVelocity = targetDir.normalized * _shotSpeed;
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
                StartCoroutine(PlayerAttack(player));
                Vector2 atkDirection = gameObject.transform.right;
                Vector2 knockBackForce = _knockBackForce;
                knockBackForce.x *= atkDirection.x;
                damageable.ApplyDamage(_damge, atkDirection, -knockBackForce, this);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayerAttack(Player player)
    {
        Debug.Log("��Ȥ����");
        player.PlayerInput.Controls.Player.Disable();
        _mover = player.GetCompo<EntityMover>();
        Vector2 targetDir = _asmodeus.transform.position - player.transform.position;
        player.transform.DOMoveX(targetDir.x, 2.5F);
        yield return new WaitForSeconds(3f);
        player.PlayerInput.Controls.Player.Enable();
        _mover.StopImmediately(true);
    }
}
