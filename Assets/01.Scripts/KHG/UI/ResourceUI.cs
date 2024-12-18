using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private Manager _manager;
    [Header("Gun")]
    [SerializeField] private Transform _bulletUI;
    [SerializeField] private TextMeshProUGUI _gunTmp;
    [Header("Resource")]
    [SerializeField] private TextMeshProUGUI _coinTmp;
    [Header("Objects")]
    [SerializeField] private GameObject _bulletPrefab;


    private void Start()
    {
        SetCoin();
        SetupGun("장착된 무기 없음", 0);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetupGun("이창호씨의 머리카락",15);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _manager.ResourceManager.Coin += 1;
            SetCoin();
        }
    }


    public void SetCoin()
    {
        _coinTmp.text = _manager.ResourceManager.Coin.ToString();
    }

    public void SetupGun(string name,int bullet)
    {
        _gunTmp.text = name.ToString();
        SetBullet(bullet);
    }

    private void SetBullet(int amount)
    {
        for (int i = 0; i < _bulletUI.childCount; i++) 
        {
            Destroy(_bulletUI.GetChild(i).gameObject);
        }

        for(int i = 0; i < amount; i++)
        {
            Instantiate(_bulletPrefab, _bulletUI);
        }
    }
}
