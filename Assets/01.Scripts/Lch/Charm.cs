using UnityEngine;

public class Charm : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float _shotSpeed = 5;
    private Rigidbody2D _rbCompo;

    private void Awake()
    {
        _target = GameObject.Find("Player").transform;
        _rbCompo = GetComponent<Rigidbody2D>();
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
            //player.PlayerInput
        }
    }
}
