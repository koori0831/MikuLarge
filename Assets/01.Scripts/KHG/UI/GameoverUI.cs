using UnityEngine;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameoverUI;
    private void Start()
    {
        _gameoverUI.SetActive(false);
    }

    public void GameOverUI()
    {
        _gameoverUI.SetActive(true);
        UIAnimPlayer("GameoverUIRaise");
    }


    public void UIAnimPlayer(string name)
    {
        _gameoverUI.GetComponent<Animator>().Play(name);
    }
}
