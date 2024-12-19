using UnityEngine;

public class ADEnemyAttackCompo : MonoBehaviour , IEntityComponent
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _fireAngle = 45f;
    [SerializeField] private Transform _shotPos;
    private float _lastAtkTime;
    private ADEnemy _adEnemys;
    [SerializeField] private Bullet _bulletPrefab;

    public void Initialize(Entity entity)
    {
        _adEnemys = entity as ADEnemy;
    }

    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

    public void Attack()
    {
        _lastAtkTime = Time.time;

        // (1 / Cos¥È) * Mathf.Sqrt( (0.5f * g * distance^2) / (distance * Tan¥È + yOffset) );
        float angle = _fireAngle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angle);
        float tan = Mathf.Tan(angle);
        float gravity = Physics2D.gravity.magnitude;
        Vector2 direction = transform.position - _adEnemys.target.transform.position;

        float distance = Mathf.Abs(direction.x);
        float yOffset = direction.y;

        float vZero = (1 / cos) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * tan + yOffset));

        float xDirection = -Mathf.Sign(direction.x);
        Vector2 velocity = new Vector3(xDirection * vZero * Mathf.Cos(angle), vZero * Mathf.Sin(angle));

        Bullet Bullet = Instantiate(_bulletPrefab, _shotPos.position, Quaternion.identity);

        Bullet.ThrowBullet(velocity, 4f);
    }
}
