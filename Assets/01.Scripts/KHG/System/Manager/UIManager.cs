using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameoverUI _gameoverUI;
    [SerializeField] private GameObject _background;
    [SerializeField] private Animator _uiAnimator;



    public void Gameover()
    {
        _gameoverUI.GameOverUI();
    }


    public void GameoverAnimPlay(string name)
    {
        _gameoverUI.UIAnimPlayer(name);
    }


    public void BackgroundClose()
    {
        _background.GetComponent<Animator>().Play("BackgroundClose");
        _uiAnimator.Play("UIClose");
    }
}
