using UnityEngine;
using UnityEngine.Events;
public class ShopText : MonoBehaviour,IInteractable
{
    [SerializeField] private Vector2 _chekerSize;
    private MainUI _textBoxOn;
    public UnityEvent OnShowText;
    public UnityEvent OnCloseText;
    [SerializeField] private LayerMask _whatIsPlayer;

    private void Awake()
    {
        _textBoxOn = FindAnyObjectByType<MainUI>();
    }

    public bool ShopChekcer()
    {
        Collider2D colider = Physics2D.OverlapBox(transform.position, _chekerSize, 0, _whatIsPlayer);
        if(colider != null)
        {
            return true;
        }
        return false;
    }

    public void Interact(Player player)
    {
        player.PlayerInput.Controls.Player.Disable();
        OnShowText?.Invoke();
    }
}
