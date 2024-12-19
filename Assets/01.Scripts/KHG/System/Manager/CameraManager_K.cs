using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using System.Collections;

public class CameraManager_K : MonoBehaviour
{
    [SerializeField] private Transform _player;
    //[SerializeField] private Manager _manager;
    [SerializeField] private Transform _mainCamObj;
    [SerializeField] private CinemachineCamera _virtualCamera;

    public float PlayerXPosition { get; set; }


    public int CurrentRoom;
    private float _moveAmount;

    private void Start()
    {
        _moveAmount = Manager.manager.MapManager_K._mapScale.x;
        CurrentRoom = 0;
    }

    public void SetCamera(float targetPosionX)
    {
        if(targetPosionX < _mainCamObj.position.x)
        {
            CurrentRoom--;
        }
        else if (targetPosionX > _mainCamObj.position.x)
        {
            CurrentRoom++;
        }
        _mainCamObj.position = new Vector3(targetPosionX, _mainCamObj.position.y, _mainCamObj.position.z);
    }


    private void FixedUpdate()
    {
        SetCameraWithInt();
    }
    //public void MoveRight()
    //{
    //    if(CurrentRoom < Manager.manager.MapManager_K._targetMapAmount)
    //    {
    //        _mainCamObj.position += new Vector3(_moveAmount, 0, 0);
    //        CurrentRoom++;
    //        SetMinimap();
    //    }
    //    //_mainCamObj.DOMoveX(_mainCamObj.position.x + _moveAmount, 0.5f);
    //}


    //public void MoveLeft()
    //{
    //    if (CurrentRoom > 0)
    //    {
    //        _mainCamObj.position += new Vector3(-_moveAmount, 0, 0);
    //        CurrentRoom--;
    //        SetMinimap();
    //    }
    //    //_mainCamObj.DOMoveX(-_mainCamObj.position.x + _moveAmount, 0.5f);
    //}


    public void ShakeCamera( float duration, float frequencyAmount, float AmplitudeAmount = 0.7f)
    {
        StartCoroutine(CamShake(duration, frequencyAmount, AmplitudeAmount));
    }

    private void SetMinimap()
    {
        Manager.manager.MinimapUI.SetMinimapPosistion(CurrentRoom);
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

    public int GetCamPos() //플레이어의 X position 을 맵 사이즈(20)에 맞게 설정해서 int 로 반환 => 예시) 10f -> 0, 25f -> 2
    {
        float adjustedPosition = PlayerXPosition + (Manager.manager.MapManager_K._mapScale.x / 2);
        if (PlayerXPosition < 10)
            return 0;
        return Mathf.FloorToInt(adjustedPosition / Manager.manager.MapManager_K._mapScale.x);
    }

    private void SetCameraWithInt()
    {
        Manager.manager.MinimapUI.SetMinimapPosistion(GetCamPos());
        PlayerXPosition = _player.position.x;
        Manager.manager.CameraManager_K.SetCamera((GetCamPos() * Manager.manager.MapManager_K._mapScale.x));
    }


    public void SetCameraAim(float time)
    {
        _virtualCamera.Lens.OrthographicSize = 4f;
        StartCoroutine(ChangeSize(time));
    }
    private IEnumerator ChangeSize(float time)
    {
        yield return new WaitForSeconds(time);
        _virtualCamera.Lens.OrthographicSize = 6f;
    }
}
