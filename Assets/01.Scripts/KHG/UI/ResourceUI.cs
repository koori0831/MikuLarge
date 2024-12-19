using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor.Media;
using JetBrains.Annotations;
using UnityEngine.UI;
using System;

public class ResourceUI : MonoSingleton<ResourceUI>
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

    [SerializeField] private Image _neail;
    private float newFillAmount = 0;

    [SerializeField] private Player _player;
    private int _bulletCount;


    private void Start()
    {
        SetCoin();
        SetupGun("장착된 무기 없음", 0);
        SetHealth(Mathf.FloorToInt(_player.health.MaxHealth));
        SetNeail(_neail);
    }


    private void Update()
    {
        switch (_player.Hands.nowWeapon)
        {
            case WeaponType.handGun:
                SetBullet(_player.Hands.currentHandGun.currentAmmo);
                break;
            case WeaponType.handsGun:
                SetBullet(_player.Hands.currentHandsGun.currentAmmo);
                break;
        }

        SetHealth(Mathf.FloorToInt(_player.health._currentHealth));

        SetNeail(_neail);

        if(newFillAmount >= 1)
        {
            Manager.manager.ResourceManager.CanNeailUse = true;
            StartCoroutine(NeaerDown());
        }
    }

    private IEnumerator NeaerDown()
    {
        while (newFillAmount <= 0)
        {
            newFillAmount = _neail.fillAmount - 0.1f;
            _neail.fillAmount = Mathf.Clamp(newFillAmount, 0, 1f);
            Manager.manager.ResourceManager.CanNeailUse = false;    
            yield return new WaitForSeconds(1);
        }
        yield return null;
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

    public void SetNeail(Image soul)
    {
        newFillAmount = soul.fillAmount + Manager.manager.ResourceManager.SoulGauge;

        soul.fillAmount = Mathf.Clamp(newFillAmount, 0f, 1f);
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

    public void SetHealth(int value)
    {
        int currentHealth = 0;
        currentHealth = value / 20;

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
