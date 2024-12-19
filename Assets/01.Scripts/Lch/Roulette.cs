using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Roulette : MonoBehaviour, IInteractable
{
    [SerializeField] private DropItemListSO _itmeList;
    [SerializeField] private SpriteRenderer _showRenderer;
    public void Interact(Player player)
    {
        Manager.manager.ResourceManager.Coin = 28;
        if (Manager.manager.ResourceManager.Coin > 25)
        {
            SpawnItems();
        }
        else
        {
            return;
        }
    }

    private void SpawnItems()
    {
        int ItemCount = Random.Range(0, _itmeList.DropItemList.Count);
        _showRenderer.sprite = _itmeList.DropItemList[ItemCount].GetComponent<SpriteRenderer>().sprite;
        Instantiate(_itmeList.DropItemList[ItemCount], transform.position, Quaternion.identity);
    }
}
