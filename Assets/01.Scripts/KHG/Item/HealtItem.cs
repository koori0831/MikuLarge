using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;

public class HealtItem : MonoBehaviour, IInteractable
{
    //[SerializeField] private Manager _manager;
    [SerializeField] private ItemSO _item;

     public bool IsShop;

    private bool isHighLight;

    private Player _player;
    public void Interact(Player player)
    {
        _player = player;
        if (Manager.manager.ResourceManager.Coin >= _item.Cost && IsShop)
        {
            Manager.manager.ResourceManager.Coin -= _item.Cost;
            Manager.manager.ResourceUI.SetCoin();
            GetItem();
            Destroy(gameObject);
        }

        if (!IsShop)
        {
            GetItem();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isHighLight == false)
        {
            HighLight(true, collision.transform);
        }
    }


    private void HighLight(bool value, Transform plr)
    {
        //print(value);
        //if (value == true)
        //{
        //    transform.DOMoveY(plr.position.y + 1, 0.5f);
        //    return;
        //}
        //transform.DOMoveY(plr.position.y - 1, 0.5f);
    }


    private void GetItem()
    {
        _player.health._currentHealth += 20;
    }
}
