using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    //[SerializeField] private Manager _manager;
    [SerializeField] private Vector3 _targetPosition;

    [SerializeField] private GameObject _stagePassUI;
    private void OnEnable()
    {
        //StartCoroutine(Intro());
    }

    private bool _enterable;
    private void Start()
    {
        DoorRise();
    }

    public void DoorRise()
    {
        transform.DOMove(_targetPosition,1.5f).SetEase(Ease.InExpo).OnComplete(()=>_enterable = true);
    }

    public void Interact(Player player)
    {
        if(_enterable)
        {
            print("TryAnimPlay");
            Manager.manager.AnimationManager.PlayerAnim("BackgroundClose");
            Manager.manager.AnimationManager.PlayerAnim("UIClose");
            StartCoroutine(StagePass());
        }
    }


    private IEnumerator StagePass()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Stage2");
    }


    private IEnumerator Intro()
    {
        _stagePassUI.SetActive(true);
        Manager.manager.AnimationManager.PlayerAnim("StagePass1");
        yield return new WaitForSeconds(6f);
        Manager.manager.AnimationManager.PlayerAnim("BackgroundOpen");
        Manager.manager.AnimationManager.PlayerAnim("UIOpen");
    }
}
