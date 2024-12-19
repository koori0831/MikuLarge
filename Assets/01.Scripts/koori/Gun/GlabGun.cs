using System;
using UnityEngine;

public class GlabGun : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject gunPreFab;
    [SerializeField] private ItemSO _itemSO;
    private Player _player;
    public bool IsShop;
    public void Interact(Player player)
    {

        _player = player;

        if (Manager.manager.ResourceManager.Coin >= _itemSO.Cost && IsShop)
        {
            Manager.manager.ResourceManager.Coin -= _itemSO.Cost;
            Manager.manager.ResourceUI.SetCoin();
            GetItem();
            Destroy(gameObject);
        }

        if (!IsShop)
        {
            player.Hands.PickUpGun(gunPreFab);
            Destroy(gameObject);
        }
       
    }

    private void GetItem()
    {
        _player.Hands.PickUpGun(gunPreFab);
        Destroy(gameObject);
    }
}
