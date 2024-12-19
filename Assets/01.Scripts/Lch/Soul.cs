using UnityEngine;

public class Soul : MonoBehaviour
{
    private Rigidbody2D _rbCompo;
    private float _lifeTime = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            Manager.manager.ResourceUI.SetNeail(0.3f);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rbCompo.AddForce(Vector2.right * 1.5f);
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
