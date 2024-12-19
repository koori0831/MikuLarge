using UnityEngine;

public class Soul : MonoBehaviour ,ICollectable
{
    private Rigidbody2D _rbCompo;
    private float _lifeTime = 10f;

    public void Collect()
    {
        Manager.manager.ResourceManager.SoulGauge += 0.3f;
        Destroy(gameObject);
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
