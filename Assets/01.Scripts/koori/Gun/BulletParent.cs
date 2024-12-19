using System;
using UnityEngine;

public abstract class BulletParent : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask enemyLayer, obstacleLayer;
    [SerializeField] private int damage;
    [SerializeField] private EntityHealthEventChannelSO bulletEnterEvent;

    public string PoolName => "Bullet";

    public GameObject ObjectPrefab => gameObject;

    public void ResetItem()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionLayer = collision.gameObject.layer;

        if (((1 << collisionLayer) & enemyLayer) != 0)
        {
            bulletEnterEvent.RaiseEvent(collision.gameObject.GetComponent<EntityHealth>());
        }

        if (((1 << collisionLayer) & obstacleLayer) != 0)
        {

        }
    }
}
