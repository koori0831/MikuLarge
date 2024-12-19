using UnityEngine;
using TMPro;
using UnityEditor.Media;
using JetBrains.Annotations;

public class ResourceUI : MonoBehaviour
{
    //[SerializeField] private Manager _manager;
    [Header("Gun")]
    [SerializeField] private Transform _bulletUI;
    [SerializeField] private TextMeshProUGUI _gunTmp;
    [Header("Resource")]
    [SerializeField] private TextMeshProUGUI _coinTmp;
    [SerializeField] private Transform _healthUI;
    [Header("Objects")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _heartPrefab;

    [SerializeField] private Player _player;


    private void Start()
    {
        SetCoin();
        SetupGun("장착된 무기 없음", 0);
        SetHealth(Manager.manager.ResourceManager.Health);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetupGun("이창호씨의 머리카락",15);
        }
    }

    private void SetBullet(int amount)
    {
        for (int i = 0; i < _bulletUI.childCount; i++)
        {
            _bulletUI.GetChild(i).GetComponent<SpriteRenderer>().color = Color.gray;
        }

        for (int i = 0; i < amount; i++)
        {
            _bulletUI.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(122, 122, 0);
        }
    }


    public void SetCoin()
    {
        _coinTmp.text = Manager.manager.ResourceManager.Coin.ToString();
    }

    public void SetupGun(string name,int bullet)
    {
        _gunTmp.text = name.ToString();
        for (int i = 0; i < _bulletUI.childCount; i++)
        {
            Destroy(_bulletUI.GetChild(i).gameObject);
        }

        for (int i = 0; i < bullet; i++)
        {
            Instantiate(_bulletPrefab, _bulletUI);
        }
    }

    public void AddHealth(int value)
    {
        Manager.manager.ResourceManager.Health += value * 20;
        SetHealth(Manager.manager.ResourceManager.Health);
    }


    public void SetHealth(int value)
    {
        int currentHealth = 0;
        currentHealth = (int)_player.GetComponent<EntityHealth>()._currentHealth;
        currentHealth = Manager.manager.ResourceManager.Health / 20;

        if (_healthUI.childCount > 0)
        {
            for (int i = 0; i < _healthUI.childCount; i++)
            {
                Destroy(_healthUI.GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(_heartPrefab, _healthUI);
        }
    }
}
