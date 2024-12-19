using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using System.Collections;

public class CameraManager_K : MonoBehaviour
{
    //[SerializeField] private Manager _manager;
    [SerializeField] private Transform _mainCamObj;
    [SerializeField] private CinemachineCamera _virtualCamera;


    private int _currentRoom;
    private float _moveAmount;

    private void Start()
    {
        _moveAmount = Manager.manager.MapManager_K._mapScale.x;
        _currentRoom = 0;
    }



    public void MoveRight()
    {
        if(_currentRoom < Manager.manager.MapManager_K._targetMapAmount)
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


    public void ShakeCamera( float duration, float frequencyAmount, float AmplitudeAmount = 0.7f)
    {
        StartCoroutine(CamShake(duration, frequencyAmount, AmplitudeAmount));
    }

    private void SetMinimap()
    {
        Manager.manager.MinimapUI.SetMinimapPosistion(_currentRoom);
    }

    private IEnumerator CamShake(float duration, float frequencyAmount, float AmplitudeAmount)
    {
        CinemachineBasicMultiChannelPerlin vCam = _virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        vCam.FrequencyGain = frequencyAmount;
        vCam.AmplitudeGain = AmplitudeAmount;
        yield return new WaitForSeconds(duration);
        vCam.FrequencyGain = 0;
        vCam.AmplitudeGain = 0;
    }
}
