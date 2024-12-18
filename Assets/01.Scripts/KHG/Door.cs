using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Manager _manager;
    [SerializeField] private Vector3 _targetPosition;

    private bool _enterable;
    private void Start()
    {
        DoorRise();
    }

    public void DoorRise()
    {
        transform.DOMove(_targetPosition,1.5f).SetEase(Ease.InExpo).OnComplete(()=>_enterable = true);
    }

    public void Interact()
    {
        if(_enterable)
        {
            print("TryAnimPlay");
            _manager.AnimationManager.PlayerAnim("UIClose");
            _manager.AnimationManager.PlayerAnim("BackgroundClose");
        }
    }
}
