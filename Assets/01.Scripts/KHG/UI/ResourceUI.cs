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
      
        SetHealth(Mathf.FloorToInt(_player.PlayerSave.CurrentHealth));
        SetNeail(0);
    }


    private void Update()
    {
        switch (_player.Hands.nowWeapon)    
        {
            case WeaponType.handGun:
                SetupGun(_player.Hands.currentHandGun.gameObject.name, _player.Hands.currentHandGun.ammo);
                break;
            case WeaponType.handsGun:
                SetupGun(_player.Hands.currentHandsGun.gameObject.name, _player.Hands.currentHandsGun.ammo);
                break;
            case WeaponType.melee:
                SetupGun("ÀåÂø ¾ÈÇÔ",0);
                break;
        }
        
        if(_player.Hands.currentHandsGun != null)
        {
            SetBullet(_player.Hands.currentHandsGun.currentAmmo);
        }

        if(_player.Hands.currentHandGun != null)
        {
            SetBullet(_player.Hands.currentHandGun.currentAmmo);
        }
        

        SetHealth(Mathf.FloorToInt(_player.health._currentHealth));

        if(newFillAmount >= 1)
        {
            Manager.manager.ResourceManager.CanNeailUse = true;
            StartCoroutine(NeaerDown());
        }
    }

    private IEnumerator NeaerDown()
    {
        yield return new WaitForSeconds(1f);
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
            _bulletUI.GetChild(i).GetComponent<Image>().color = Color.gray;
        }

        for (int i = 0; i < amount; i++)
        {
            _bulletUI.GetChild(i).GetComponent<Image>().color = new Color(122, 122, 0);
        }
    }

    public void SetNeail(float soul)
    {
        newFillAmount = _neail.fillAmount +soul;

        _neail.fillAmount = Mathf.Clamp(newFillAmount, 0f, 1f);
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
