using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int Coin { get; set; } = 0;
    public float SoulGauge { get; set; } = 0;
    public bool CanNeailUse { get; set; } = false;

    private void OnEnable()
    {
        if (GameManager.Instance.SaveSo != null)
            Coin = GameManager.Instance.SaveSo.NowCoin;
    }
}
