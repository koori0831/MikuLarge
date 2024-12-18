using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Manager _manager;
    [SerializeField] private Vector3 _targetPosition;

    [SerializeField] private GameObject _stagePassUI;

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
            _manager.AnimationManager.PlayerAnim("BackgroundClose");
            _manager.AnimationManager.PlayerAnim("UIClose");
            StartCoroutine(StagePass());
        }
    }


    private IEnumerator StagePass()
    {
        yield return new WaitForSeconds(1.5f);
        _stagePassUI.SetActive(true);
        _manager.AnimationManager.PlayerAnim("StagePass1");
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Stage2");
    }
}
