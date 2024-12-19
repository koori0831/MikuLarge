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





using UnityEngine;
using DG.Tweening;

public class RoomDivider : MonoBehaviour
{
    [SerializeField] private Vector2 detectSize; // 적 감지 영역 크기
    [SerializeField] private Transform origin; // 감지 영역 기준 위치
    [SerializeField] private LayerMask enemyLayer; // 적으로 감지할 레이어
    [SerializeField] private Transform door; // 문 오브젝트

    private bool isClosed; // 문이 닫혀있는지 여부

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return; // 플레이어가 아니면 반환
        if (isClosed) return; // 이미 닫혀있으면 반환

        DetectAndCloseDoor(); // 적 감지 후 문 닫기
    }

    private void FixedUpdate()
    {
        if (isClosed) DetectEnemies(); // 문이 닫힌 상태에서 적 상태를 확인
    }

    private void DetectAndCloseDoor()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(origin.position, detectSize, 0, enemyLayer);

        if (enemies.Length > 0) // 감지된 적이 있으면
        {
            CloseDoor();
        }
    }

    private void DetectEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(origin.position, detectSize, 0, enemyLayer);

        if (enemies.Length == 0) // 감지된 적이 없으면
        {
            OpenDoor();
        }
    }

    private void CloseDoor()
    {
        if (isClosed) return; // 이미 닫혀있으면 반환

        isClosed = true; // 문 상태 변경
        door.DOMoveY(door.position.y - 4f, 0.1f).OnComplete(() =>
        {
            Manager.manager.CameraManager_K.ShakeCamera(0.2f, 2f);
        });
    }

    private void OpenDoor()
    {
        if (!isClosed) return; // 이미 열려있으면 반환

        isClosed = false; // 문 상태 변경
        door.DOMoveY(door.position.y + 4f, 0.1f).OnComplete(() =>
        {
            Manager.manager.CameraManager_K.ShakeCamera(0.2f, 2f);
        });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(origin.position, detectSize); // 적 감지 영역 시각화
    }
}
