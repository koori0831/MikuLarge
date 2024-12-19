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
        _clearedRoom.Add(0); // �ʱ� �� ���� �߰�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return; // �÷��̾ �ƴϸ� ��ȯ
        if (_clearedRoom.Contains(Manager.manager.CameraManager_K.CurrentRoom)) return; // �̹� Ŭ����� ���̸� ��ȯ
        if (_isClosed) return; // �̹� ���� ���������� ��ȯ
        PlayerEntered();
    }

    public void PlayerEntered()
    {
        if (!_isClosed) // ���� ���������� �ݱ�
        {
            DetectAndSetDoor(false);
        }
    }

    private void DetectAndSetDoor(bool value)
    {
        if (value) // �� ����
        {
            _isClosed = false;
            Manager.manager.RoomManager.DoorStatus = false;
            DoorOpen();
        }
        else // �� �ݱ�
        {
            // �� ����
            Collider2D[] enemys = Physics2D.OverlapBoxAll(_origin.position, _detectSize, 0, _enemyLayer);
            if (enemys.Length > 0) // ���� ������ �ݱ�
            {
                _isClosed = true;
                Manager.manager.RoomManager.DoorStatus = true;
                DoorClose();
            }
        }
    }

    private void DoorClose()
    {
        if (_door.position.y > 1f) return; // ���� �̹� ���� ���¸� ��ȯ
        _door.DOMoveY(_door.position.y - 4f, 0.1f).OnComplete(() =>
            Manager.manager.CameraManager_K.ShakeCamera(0.2f, 2f));
    }

    private void DoorOpen()
    {
        if (_door.position.y < 3f) return; // ���� �̹� ���� ���¸� ��ȯ
        _door.DOMoveY(_door.position.y + 4f, 0.1f);
    }

    private void Detect()
    {
        if (!_isClosed) return; // ���� ���������� �� ���� ���ʿ�

        Collider2D[] enemys = Physics2D.OverlapBoxAll(_origin.position, _detectSize, 0, _enemyLayer);
        if (enemys.Length == 0) // ������ ���� ������
        {
            DetectAndSetDoor(true); // �� ����
            _clearedRoom.Add(Manager.manager.CameraManager_K.CurrentRoom); // ���� ���� Ŭ���� ��Ͽ� �߰�
        }
    }

    private void FixedUpdate()
    {
        Detect(); // �� �����Ӹ��� �� ����
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_origin.position, _detectSize); // �� ���� ���� �ð�ȭ
    }
}
