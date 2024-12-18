using System;
using UnityEngine;

public enum WeaponType
{
    handGun, handsGun, melee
}

public class Hands : MonoBehaviour
{
    [SerializeField] Sprite _handGun, _handsGun, _melee;
    private SpriteRenderer _handRenderer;

    private WeaponType _nowWeapon = WeaponType.melee;
    private Player _player;
    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _handRenderer = GetComponent<SpriteRenderer>();
        _player.PlayerInput.ChangeWeaponEvent += WeaponChange;
        ImageChange();
    }


    public void WeaponChange()
    {
        _nowWeapon = GetNext(_nowWeapon);
        ImageChange();
    }

    private void FixedUpdate()
    {
        if (_nowWeapon != WeaponType.melee)
        {
            // 마우스 위치를 월드 좌표로 변환
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z; // Z값을 현재 오브젝트의 Z로 고정

            // 마우스 방향 계산
            Vector3 direction = mousePosition - transform.position;

            // 방향에서 각도 계산 (라디안을 각도로 변환)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Z축 회전을 적용
            transform.localEulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }

    private void ImageChange()
    {
        switch (_nowWeapon)
        {
            case WeaponType.handGun:_handRenderer.sprite = _handGun; break;
            case WeaponType.handsGun: _handRenderer.sprite = _handsGun; break;
            case WeaponType.melee:_handRenderer.sprite = _melee; break;
        }
    }

    private void OnDestroy()
    {
        _player.PlayerInput.ChangeWeaponEvent -= WeaponChange;
    }
    public static T GetNext<T>(T enumValue) where T : Enum
    {
        Array array = System.Enum.GetValues(typeof(T));
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (enumValue.Equals(array.GetValue(i)))
                return (T)array.GetValue(i + 1);
        }
        return (T)array.GetValue(0);
    }
}
