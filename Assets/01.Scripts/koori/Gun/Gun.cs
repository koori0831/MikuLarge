using Unity.Cinemachine;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] private EntityHealthEventChannelSO bulletEnterEvent;
    public Transform firePos; 
    public WeaponType type;
    public BulletParent bullet;
    public float damage;
    public Vector2 knockBack;
    public float reloadingTime;
    public int ammo;
    public int shotCount = 1;
    [Range(0,360)]
    public int spreadAngle = 0;

    private Player _player;

    private void OnEnable()
    {
        _player = GetComponentInParent<Player>();
        bulletEnterEvent.OnEventRaised += DamageCalculate;
    }

    public virtual void Shot()
    {
        
        Debug.Log("»§¾ß");
    }

    private void DamageCalculate(EntityHealth target)
    {
        target.ApplyDamage(damage, Vector2.right, knockBack, _player);
    }

    private void OnDisable()
    {
        _player = null;
        bulletEnterEvent.OnEventRaised -= DamageCalculate;
    }
}
