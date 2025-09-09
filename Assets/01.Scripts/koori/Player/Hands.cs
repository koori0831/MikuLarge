using System;
using UnityEngine;

public enum WeaponType
{
    handGun, handsGun, melee
}

public class Hands : MonoBehaviour, IEntityComponent
{
    [SerializeField] Sprite _handGun, _handsGun, _melee;
    private SpriteRenderer _handRenderer;
    private Entity _entity;
    [SerializeField] private Transform _handTransform, _handsTransform;

    public NotifyValue<WeaponType> nowWeapon;
    private Player _player;
    public Gun currentHandGun;
    public Gun currentHandsGun;
    public GameObject picked;
    private void Awake()
    {
        nowWeapon.Value = WeaponType.melee;
        _player = GetComponentInParent<Player>();
        _handRenderer = GetComponent<SpriteRenderer>();
        _player.PlayerInput.ChangeWeaponEvent += WeaponChange;
        ImageChange();
    }


    public void WeaponChange()
    {
        if(_player.isReloading || _player.isHit) return;
        
        WeaponType nextWeapon = GetNextAvailableWeapon(nowWeapon.Value);
        if (nextWeapon != nowWeapon.Value) // 실제로 변경될 무기가 있을 때만 변경
        {
            nowWeapon.Value = nextWeapon;
            ImageChange();
        }
    }

    private void FixedUpdate()
    {
        if (nowWeapon.Value != WeaponType.melee)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;

            Vector3 direction = mousePosition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           
            if (Mathf.Sign(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x) < 0)
            {
                transform.localEulerAngles = new Vector3(0, 0, -(angle+180));
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, angle);
            }
        }

        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }

    private void ImageChange()
    {
        switch (nowWeapon.Value)
        {
            case WeaponType.handGun:
                _handRenderer.sprite = _handGun;
                _handTransform.gameObject.SetActive(true);
                _handsTransform.gameObject.SetActive(false); 
                break;
            case WeaponType.handsGun:
                _handRenderer.sprite = _handsGun;
                _handTransform.gameObject.SetActive(false);
                _handsTransform.gameObject.SetActive(true); 
                break;
            case WeaponType.melee:
                _handRenderer.sprite = _melee;
                _handTransform.gameObject.SetActive(false);
                _handsTransform.gameObject.SetActive(false); 
                break;
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

    public void PickUpGun(GameObject Picked)
    {
         if (currentHandGun != null)
            Destroy(currentHandGun.gameObject);
         if(currentHandsGun != null)
            Destroy(currentHandsGun.gameObject);
         picked = Picked;

         Gun gunCompo = Picked.GetComponent<Gun>();


        switch (gunCompo.type)
        {
            case WeaponType.handGun: 
                GameObject HandGun = Instantiate(Picked, _handTransform);
                HandGun.name = Picked.name;
                currentHandGun = HandGun.GetComponent<Gun>(); ;
                nowWeapon.Value = WeaponType.handGun; ImageChange(); break;
            case WeaponType.handsGun: 
                GameObject HandsGun = Instantiate(Picked, _handsTransform);
                HandsGun.name = Picked.name;
                currentHandsGun = HandsGun.GetComponent<Gun>();
                nowWeapon.Value = WeaponType.handsGun; ImageChange(); break;
        }
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    private WeaponType GetNextAvailableWeapon(WeaponType current)
    {
        WeaponType startType = current;
        WeaponType nextType = GetNext(current);
        
        // 한 바퀴 돌 때까지 확인
        do
        {
            if (IsWeaponAvailable(nextType))
                return nextType;
            
            nextType = GetNext(nextType);
        } while (nextType != startType);
        
        // 사용 가능한 무기가 없으면 현재 무기 유지
        return current;
    }

    private bool IsWeaponAvailable(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.handGun:
                return currentHandGun != null;
            case WeaponType.handsGun:
                return currentHandsGun != null;
            case WeaponType.melee:
                return true; // 맨손은 항상 사용 가능
            default:
                return false;
        }
    }
}
