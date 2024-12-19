using UnityEngine;

public class Soul : MonoBehaviour ,ICollectable
{
    private Rigidbody2D _rbCompo;

    public void Collect()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rbCompo.AddForce(Vector2.right * 1.5f);
    }
}
