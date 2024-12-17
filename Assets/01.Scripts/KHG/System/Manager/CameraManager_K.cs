using UnityEngine;
using DG.Tweening;

public class CameraManager_K : MonoBehaviour
{
    [SerializeField] private Manager _manager;
    [SerializeField] private Transform _mainCamObj;


    private int _currentRoom;
    private float _moveAmount;

    private void Start()
    {
        _moveAmount = _manager.MapManager_K._mapScale.x;
        _currentRoom = 0;
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            MoveLeft();
        }
    }


    public void MoveRight()
    {
        _mainCamObj.position += new Vector3(_moveAmount, 0, 0);
        //_mainCamObj.DOMoveX(_mainCamObj.position.x + _moveAmount, 0.5f);
    }


    public void MoveLeft()
    {
        _mainCamObj.position += new Vector3(-_moveAmount, 0, 0);
        //_mainCamObj.DOMoveX(-_mainCamObj.position.x + _moveAmount, 0.5f);
    }
}
