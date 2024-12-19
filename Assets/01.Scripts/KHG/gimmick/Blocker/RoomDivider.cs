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
//        if(_isClosed == false) //�����ִ� ->
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
//        if(enemys.Length <= 0 && _isClosed) //������ ���� 0���� �۰� ���� ����������
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
    [SerializeField] private Vector2 detectSize; // �� ���� ���� ũ��
    [SerializeField] private Transform origin; // ���� ���� ���� ��ġ
    [SerializeField] private LayerMask enemyLayer; // ������ ������ ���̾�
    [SerializeField] private Transform door; // �� ������Ʈ

    private bool isClosed; // ���� �����ִ��� ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return; // �÷��̾ �ƴϸ� ��ȯ
        if (isClosed) return; // �̹� ���������� ��ȯ

        DetectAndCloseDoor(); // �� ���� �� �� �ݱ�
    }

    private void FixedUpdate()
    {
        if (isClosed) DetectEnemies(); // ���� ���� ���¿��� �� ���¸� Ȯ��
    }

    private void DetectAndCloseDoor()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(origin.position, detectSize, 0, enemyLayer);

        if (enemies.Length > 0) // ������ ���� ������
        {
            CloseDoor();
        }
    }

    private void DetectEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(origin.position, detectSize, 0, enemyLayer);

        if (enemies.Length == 0) // ������ ���� ������
        {
            OpenDoor();
        }
    }

    private void CloseDoor()
    {
        if (isClosed) return; // �̹� ���������� ��ȯ

        isClosed = true; // �� ���� ����
        door.DOMoveY(door.position.y - 4f, 0.1f).OnComplete(() =>
        {
            Manager.manager.CameraManager_K.ShakeCamera(0.2f, 2f);
        });
    }

    private void OpenDoor()
    {
        if (!isClosed) return; // �̹� ���������� ��ȯ

        isClosed = false; // �� ���� ����
        door.DOMoveY(door.position.y + 4f, 0.1f).OnComplete(() =>
        {
            Manager.manager.CameraManager_K.ShakeCamera(0.2f, 2f);
        });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(origin.position, detectSize); // �� ���� ���� �ð�ȭ
    }
}
