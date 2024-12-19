using System;
using UnityEngine;

public abstract class BulletParent : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer, obstacleLayer;
    [SerializeField] private EntityHealthEventChannelSO bulletEnterEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int collisionLayer = collision.gameObject.layer;

        if (((1 << collisionLayer) & enemyLayer) != 0)
        {
            bulletEnterEvent.RaiseEvent(collision.gameObject.GetComponent<EntityHealth>());
        }

        if (((1 << collisionLayer) & obstacleLayer) != 0)
        {
            Destroy(gameObject);
        }
    }
}
