using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Roulette : MonoBehaviour, IInteractable
{
    [SerializeField] private DropItemListSO _itmeList;
    [SerializeField] private SpriteRenderer _showRenderer;
    private Animator _animator;
    private int ItemCount;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact(Player player)
    {
        Manager.manager.ResourceManager.Coin = 28;
        if (Manager.manager.ResourceManager.Coin > 25)
        {
            _animator.SetBool("Roll", true);
        }
        else
        {
            return;
        }
    }

    public void SpawnItems()
    {
         ItemCount = Random.Range(0, _itmeList.DropItemList.Count);
        _showRenderer.sprite = _itmeList.DropItemList[ItemCount].GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(DropItmeShot());
       
    }

    private IEnumerator DropItmeShot()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(_itmeList.DropItemList[ItemCount], transform.position, Quaternion.identity);
        _animator.SetBool("Roll", false);
    }
}