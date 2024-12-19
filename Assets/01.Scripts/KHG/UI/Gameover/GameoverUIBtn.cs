using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverUIBtn : MonoBehaviour
{

    private bool _btnClickable = true;
    private void OnEnable()
    {
        Manager.manager.UIManager.BackgroundClose();

    }
    public void RestartClicked()
    {
        if (_btnClickable)
        {
            _btnClickable = false;
            Manager.manager.UIManager.GameoverAnimPlay("GameoverUIDisapear");
            StartCoroutine(DelaySceneLoad("Stage1", 1f));
        }
    }
    public void QuitClicked()
    {
        if (_btnClickable)
        {
            _btnClickable = false;
            Manager.manager.UIManager.GameoverAnimPlay("GameoverUIDisapear");
            StartCoroutine(DelaySceneLoad("Menu", 1f));
        }
    }

    private IEnumerator DelaySceneLoad(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        Manager.manager.DestoryManager();
        SceneManager.LoadScene(sceneName);
    }
}
