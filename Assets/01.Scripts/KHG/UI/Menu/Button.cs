using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] private Animator _backgroundAnimator;
    [SerializeField] private Animator _btnAnimator;

    private bool _btnClickable = true;
    public void StartClicked()
    {
        if (_btnClickable)
        {
            _btnClickable = false;
            AnimPlay(_backgroundAnimator, "BackgroundUIMenu_Start");
            AnimPlay(_btnAnimator, "ButtonUI_Close");
            StartCoroutine(DelaySceneLoad("Stage1", 1.5f));
        }
    }

    public void SettingClicked()
    {
        //if (_btnClickable)
        //{
        //    _btnClickable = false;
        //}
    }

    public void QuitClicked()
    {
        if (_btnClickable)
        {
            _btnClickable = false;
            Application.Quit();
        }
    }

    private void AnimPlay(Animator animator, string name)
    {
        animator.Play(name);
    }

    private IEnumerator DelaySceneLoad(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
