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
        if(_currentRoom < _manager.MapManager_K._targetMapAmount)
        {
            _mainCamObj.position += new Vector3(_moveAmount, 0, 0);
            _currentRoom++;
            SetMinimap();
        }
        //_mainCamObj.DOMoveX(_mainCamObj.position.x + _moveAmount, 0.5f);
    }


    public void MoveLeft()
    {
        if (_currentRoom > 0)
        {
            _mainCamObj.position += new Vector3(-_moveAmount, 0, 0);
            _currentRoom--;
            SetMinimap();
        }
        //_mainCamObj.DOMoveX(-_mainCamObj.position.x + _moveAmount, 0.5f);
    }

    private void SetMinimap()
    {
        _manager.MinimapUI.SetMinimapPosistion(_currentRoom);
    }
}
