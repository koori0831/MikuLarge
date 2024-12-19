using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;

public class RoomDivider : MonoBehaviour
{
    [SerializeField] private Vector2 _detectSize;
    [SerializeField] private Transform _origin;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _door;

    private List<GameObject> _enemyList = new List<GameObject>();
    private bool _isClosed;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerEntered();
        }
    }

    public void PlayerEntered()
    {
        if(_isClosed == false)
        {
            SetDoor(false);
        }
    }

    private void SetDoor(bool value)
    {
        if(value) //open
        {
            _isClosed = false;
            DoorOpen();
        }
        else //close
        {
            _isClosed = true;
            Detect();
            DoorClose();
        }
    }


    private void DoorClose()
    {
        _door.DOMoveY(_door.position.y - 4f, 0.1f);
    }
    private void DoorOpen()
    {
        _door.DOMoveY(_door.position.y + 4f, 0.1f);
    }

    private void Detect()
    {
        Collider2D[] enemys = Physics2D.OverlapBoxAll(_origin.position , _detectSize,0, _enemyLayer);
        if(enemys.Length <= 0 && _isClosed) //감지된 적이 0보다 작고 문이 닫혀있을떄
        {
            SetDoor(true);
        }
    }


    private void FixedUpdate()
    {
        Detect();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_origin.position, _detectSize);
    }
}
