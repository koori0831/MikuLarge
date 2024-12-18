using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        CoinCollected();
    }

    private void CoinCollected()
    {
        Manager.manager.ResourceManager.Coin++;
        Manager.manager.ResourceUI.SetCoin();
        Destroy(gameObject);
    }
}
