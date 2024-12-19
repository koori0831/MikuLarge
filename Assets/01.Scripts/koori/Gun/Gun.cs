using Ami.BroAudio;
using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] private EntityHealthEventChannelSO bulletEnterEvent;
    [SerializeField] private GameObject bulletPrefab;
    public Transform firePos;
    public WeaponType type;
    public float damage;
    public float shotSpeed;
    public float shotWaitTime;
    public Vector2 knockBack;
    public float reloadingTime;
    public int ammo;
    public int shotCount = 1;
    [Range(0, 360)]
    public int spreadAngle = 0;

    public int currentAmmo;
    private bool isReloading;
    private Coroutine reloadCoroutine;
    private Player _player;
    private float nextShotTime;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        currentAmmo = ammo;
    }

    private void OnEnable()
    {
        bulletEnterEvent.OnEventRaised += DamageCalculate;
        _player.PlayerInput.ReloadEvent += StartReloading;
    }

    private void OnDisable()
    {
        bulletEnterEvent.OnEventRaised -= DamageCalculate;
        _player.PlayerInput.ReloadEvent -= StartReloading;
    }

    public virtual void Shot()
    {
        if (Time.time < nextShotTime)
        {
            Debug.Log("Shot is on cooldown.");
            return;
        }

        if (isReloading)
        {
            Debug.Log("Reloading in progress.");
            return;
        }

        if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo, reloading...");
            StartReloading();
            return;
        }

        currentAmmo--;
        nextShotTime = Time.time + shotWaitTime;

        Manager.manager.CameraManager_K.ShakeCamera(0.1f, 0.1f);

        if (shotCount > 1)
        {
            MultiShot();
        }
        else
        {
            SingleShot();
        }
    }

    private void SingleShot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = firePos.right * shotSpeed;
    }

    private void MultiShot()
    {
        float angleStep = spreadAngle / (shotCount - 1);
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < shotCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, firePos.eulerAngles.z + angle);
            GameObject bullet = Instantiate(bulletPrefab, firePos.position, rotation);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = rotation * Vector2.right * shotSpeed;
        }
    }

    private void StartReloading()
    {
        if (isReloading||_player.isReloading ||_player.isHit) return;

        isReloading = true;
        _player.isReloading = true;
        _player.ReLoadOb.SetActive(true);
        reloadCoroutine = StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadingTime);
        currentAmmo = ammo;
        isReloading = false;
        _player.isReloading = false;
        _player.ReLoadOb.SetActive(false);
        Debug.Log("Reload complete!");
    }

    private void DamageCalculate(EntityHealth target)
    {
        target.ApplyDamage(damage, Vector2.right, knockBack, _player);
    }
}
