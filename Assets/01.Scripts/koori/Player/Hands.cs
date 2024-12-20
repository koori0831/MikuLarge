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

    public WeaponType nowWeapon = WeaponType.melee;
    private Player _player;
    public Gun currentHandGun;
    public Gun currentHandsGun;
    public GameObject picked;
    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _handRenderer = GetComponent<SpriteRenderer>();
        _player.PlayerInput.ChangeWeaponEvent += WeaponChange;
        ImageChange();
    }


    public void WeaponChange()
    {
        if(_player.isReloading || _player.isHit) return;
        nowWeapon = GetNext(nowWeapon);
        ImageChange();
    }

    private void FixedUpdate()
    {
        if (nowWeapon != WeaponType.melee)
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
        switch (nowWeapon)
        {
            case WeaponType.handGun:if (currentHandGun == null)
                {
                    nowWeapon = GetNext(nowWeapon);
                    ImageChange();
                    break;
                }
                _handRenderer.sprite = _handGun;
                _handTransform.gameObject.SetActive(true);
                _handsTransform.gameObject.SetActive(false); break;
            case WeaponType.handsGun:if (currentHandsGun == null)
                {
                    nowWeapon = GetNext(nowWeapon);
                    ImageChange();
                    break;
                }
                _handRenderer.sprite = _handsGun;
                _handTransform.gameObject.SetActive(false);
                _handsTransform.gameObject.SetActive(true); break;
            case WeaponType.melee:_handRenderer.sprite = _melee;
                _handTransform.gameObject.SetActive(false);
                _handsTransform.gameObject.SetActive(false); break;
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
                nowWeapon = WeaponType.handGun; ImageChange(); break;
            case WeaponType.handsGun: 
                GameObject HandsGun = Instantiate(Picked, _handsTransform);
                HandsGun.name = Picked.name;
                currentHandsGun = HandsGun.GetComponent<Gun>();
                nowWeapon = WeaponType.handsGun; ImageChange(); break;
        }
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }
}
