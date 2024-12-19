using UnityEngine;

public class ADEnemyAttackCompo : MonoBehaviour , IEntityComponent
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _fireAngle = 45f;
    [SerializeField] private Transform _shotPos;
    private float _lastAtkTime;
    private ADEnemy _adEnemys;
    [SerializeField] private Bullet _bulletPrefab;
    Vector2 velocity;

    public void Initialize(Entity entity)
    {
        _adEnemys = entity as ADEnemy;
    }

    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

    public void Attack()
    {
        _lastAtkTime = Time.time;

        float angle = _fireAngle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angle);
        if (Mathf.Approximately(cos, 0f))
        {
            Debug.LogError("Cosine of angle is zero, division by zero detected!");
            return;
        }

        float tan = Mathf.Tan(angle);
        float gravity = Physics2D.gravity.magnitude;
        Vector2 direction = transform.position - _adEnemys.target.transform.position;

        float distance = Mathf.Abs(direction.x);
        float yOffset = direction.y;

        // 플레이어가 적 바로 위에 있는 경우 처리
        if (Mathf.Approximately(distance, 0f))
        {
            Debug.LogWarning("Player is directly above the enemy. Adjusting attack behavior.");
            velocity = new Vector2(0f, Mathf.Sqrt(2 * gravity * Mathf.Abs(yOffset))); // 수직 발사 속도 계산
            Bullet bullet = Instantiate(_bulletPrefab, _shotPos.position, Quaternion.identity);
            bullet.ThrowBullet(velocity, 4f);
            return;
        }
        float denominator = distance * tan + yOffset;
        if (denominator <= 0f)
        {
            Debug.LogWarning($"Adjusting attack: Invalid denominator: {denominator}. Adjusting target angle or velocity.");
            // 대체 로직 추가
            AdjustVerticalAttack(yOffset, gravity);
            return;
        }

        float sqrtInput = (0.5f * gravity * Mathf.Pow(distance, 2)) / denominator;
        if (sqrtInput < 0f)
        {
            Debug.LogError("Invalid square root input: " + sqrtInput);
            return;
        }

        float vZero = (1 / cos) * Mathf.Sqrt(sqrtInput);

        float xDirection = -Mathf.Sign(direction.x);
        velocity = new Vector3(xDirection * vZero * Mathf.Cos(angle), vZero * Mathf.Sin(angle));
        Bullet bulletNormal = Instantiate(_bulletPrefab, _shotPos.position, Quaternion.identity);

        bulletNormal.ThrowBullet(velocity, 4f);
    }

    private void AdjustVerticalAttack(float yOffset, float gravity)
    {
        // 수직으로 발사할 속도 계산
        float verticalSpeed = Mathf.Sqrt(2 * gravity * Mathf.Abs(yOffset));
        velocity = new Vector2(0, verticalSpeed);

        Bullet bullet = Instantiate(_bulletPrefab, _shotPos.position, Quaternion.identity);
        bullet.ThrowBullet(velocity, 4f);

        Debug.Log($"Adjusted vertical attack with velocity: {velocity}");
    }
}
