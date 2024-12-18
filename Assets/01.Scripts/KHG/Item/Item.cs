using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour, IInteractable
{
    //[SerializeField] private Manager _manager;
    [SerializeField] private ItemSO _item;

    private bool isHighLight;
    public void Interact()
    {
        if (Manager.manager.ResourceManager.Coin >= _item.Cost)
        {
            Manager.manager.ResourceManager.Coin -= _item.Cost;
            Manager.manager.ResourceUI.SetCoin();
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
        print("æ∆¿Ã≈€ »π!µÊ");
    }
}
