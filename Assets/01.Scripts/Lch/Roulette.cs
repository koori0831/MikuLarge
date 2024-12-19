using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Roulette : MonoBehaviour, IInteractable
{
    [SerializeField] private DropItemListSO _itmeList;
    [SerializeField] private SpriteRenderer _showRenderer;
    [SerializeField] private Transform _shotPos;
    private Animator _animator;
    private int ItemCount;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact(Player player)
    {

        if (Manager.manager.ResourceManager.Coin < 10)
        {
            return;
        }

        if (Manager.manager.ResourceManager.Coin >= 10)
        {
            _animator.SetBool("Roll", true);
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
        Instantiate(_itmeList.DropItemList[ItemCount], _shotPos.position, Quaternion.identity,null);
        _animator.SetBool("Roll", false);
    }
}
