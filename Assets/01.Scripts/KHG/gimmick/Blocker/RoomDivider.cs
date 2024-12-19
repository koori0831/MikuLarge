//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using DG.Tweening;
//using UnityEngine;

//public class RoomDivider : MonoBehaviour
//{
//    [SerializeField] private Vector2 _detectSize;
//    [SerializeField] private Transform _origin;
//    [SerializeField] private LayerMask _enemyLayer;
//    [SerializeField] private Transform _door;

//    private List<GameObject> _enemyList = new List<GameObject>();
//    private List<int> _clearedRoom = new List<int>();
//    private bool _isClosed;

//    private void Start()
//    {
//        _clearedRoom.Add(0);
//    }


//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player") == false) return;
//        if (_clearedRoom.Contains(Manager.manager.CameraManager_K.CurrentRoom)) return;
//        if (_isClosed) return;
//        PlayerEntered();
//    }


//    public void PlayerEntered()
//    {
//        if(_isClosed == false) //열려있다 ->
//        {
//            SetDoor(false);
//        }
//    }

//    private void SetDoor(bool value)
//    {
//        if(value) //open
//        {
//            _isClosed = false;
//            Manager.manager.RoomManager.DoorStatus = false;
//            DoorOpen();
//        }
//        else //close
//        {
//            _isClosed = true;
//            Manager.manager.RoomManager.DoorStatus = true;
//            Detect();
//            DoorClose();
//        }
//    }


//    private void DoorClose()
//    {
//        if (_door.position.y > 1f) return;
//        _door.DOMoveY(_door.position.y - 4f, 0.1f).OnComplete(()=>Manager.manager.CameraManager_K.ShakeCamera(0.2f,2f));
//    }
//    private void DoorOpen()
//    {
//        if (_door.position.y < 3f) return;
//        _door.DOMoveY(_door.position.y + 4f, 0.1f);
//    }

//    private void Detect()
//    {
//        Collider2D[] enemys = Physics2D.OverlapBoxAll(_origin.position , _detectSize,0, _enemyLayer);
//        print(enemys.Length + _isClosed.ToString());
//        if(enemys.Length <= 0 && _isClosed) //감지된 적이 0보다 작고 문이 닫혀있을떄
//        {
//            SetDoor(true);
//            _clearedRoom.Add(Manager.manager.CameraManager_K.CurrentRoom);
//        }
//    }


//    private void FixedUpdate()
//    {
//        Detect();
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.blue;
//        Gizmos.DrawWireCube(_origin.position, _detectSize);
//    }
//}





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
    private List<int> _clearedRoom = new List<int>();
    private bool _isClosed;

    private void Start()
    {
        _clearedRoom.Add(0); // 초기 방 상태 추가
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return; // 플레이어가 아니면 반환
        if (_clearedRoom.Contains(Manager.manager.CameraManager_K.CurrentRoom)) return; // 이미 클리어된 방이면 반환
        if (_isClosed) return; // 이미 문이 닫혀있으면 반환
        PlayerEntered();
    }

    public void PlayerEntered()
    {
        if (!_isClosed) // 문이 열려있으면 닫기
        {
            DetectAndSetDoor(false);
        }
    }

    private void DetectAndSetDoor(bool value)
    {
        if (value) // 문 열기
        {
            _isClosed = false;
            Manager.manager.RoomManager.DoorStatus = false;
            DoorOpen();
        }
        else // 문 닫기
        {
            // 적 감지
            Collider2D[] enemys = Physics2D.OverlapBoxAll(_origin.position, _detectSize, 0, _enemyLayer);
            if (enemys.Length > 0) // 적이 있으면 닫기
            {
                _isClosed = true;
                Manager.manager.RoomManager.DoorStatus = true;
                DoorClose();
            }
        }
    }

    private void DoorClose()
    {
        if (_door.position.y > 1f) return; // 문이 이미 닫힌 상태면 반환
        _door.DOMoveY(_door.position.y - 4f, 0.1f).OnComplete(() =>
            Manager.manager.CameraManager_K.ShakeCamera(0.2f, 2f));
    }

    private void DoorOpen()
    {
        if (_door.position.y < 3f) return; // 문이 이미 열린 상태면 반환
        _door.DOMoveY(_door.position.y + 4f, 0.1f);
    }

    private void Detect()
    {
        if (!_isClosed) return; // 문이 열려있으면 적 감지 불필요

        Collider2D[] enemys = Physics2D.OverlapBoxAll(_origin.position, _detectSize, 0, _enemyLayer);
        if (enemys.Length == 0) // 감지된 적이 없으면
        {
            DetectAndSetDoor(true); // 문 열기
            _clearedRoom.Add(Manager.manager.CameraManager_K.CurrentRoom); // 현재 방을 클리어 목록에 추가
        }
    }

    private void FixedUpdate()
    {
        Detect(); // 매 프레임마다 적 감지
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_origin.position, _detectSize); // 적 감지 영역 시각화
    }
}
