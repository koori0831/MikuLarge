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
            // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z; // Z���� ���� ������Ʈ�� Z�� ����

            // ���콺 ���� ���
            Vector3 direction = mousePosition - transform.position;

            // ���⿡�� ���� ��� (������ ������ ��ȯ)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Z�� ȸ���� ����
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
