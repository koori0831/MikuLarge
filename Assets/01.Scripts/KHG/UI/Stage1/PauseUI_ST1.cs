using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseUI_ST1 : MonoBehaviour
{
    [SerializeField] private GameObject _pauseUI;

    private bool _isOpen;
    private bool _isCool;

    private void Start()
    {
        _pauseUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isCool == false)
        {
            if (_isOpen == false)
            {
                _isOpen = true;
                _pauseUI.SetActive(true);
                UIPlayer("PauseUIOpen");
            }
            else
            {
                _isOpen = false;
                _pauseUI.SetActive(true);
                ContinueClicked();
            }
            Cool(1.5f);
        }
    }



    public void ContinueClicked()
    {
        UIPlayer("PauseUIClose");
        StartCoroutine(Close(1f));
    }
    public void QuitClicked()
    {
        UIPlayer("PauseUIQuit");
        StartCoroutine(Quit(1f));
    }

    private IEnumerator Quit(float time)
    {
        yield return new WaitForSeconds(time);
        Manager.manager.DestoryManager();
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator Close(float time)
    {
        yield return new WaitForSeconds(time);
        _pauseUI.SetActive(false);

    }

    private void UIPlayer(string name)
    {
        _pauseUI.GetComponent<Animator>().Play(name);
    }

    private IEnumerator Cool(float time)
    {
        _isCool = true;
        yield return new WaitForSeconds(time);
        _isCool = false;
    }
}
