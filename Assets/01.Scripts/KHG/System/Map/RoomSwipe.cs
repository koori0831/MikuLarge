using System;
using UnityEngine;

public class RoomSwipe : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x > transform.position.x)
            {
                Manager.manager.CameraManager_K.MoveLeft();
            }
            else
            {
                Manager.manager.CameraManager_K.MoveRight();
            }
        }
    }
}
