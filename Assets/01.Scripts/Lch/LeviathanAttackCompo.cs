using UnityEngine;

public class LeviathanAttackCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float _cooldown;
    private float _lastAtkTime;
    private Leviathan _leviathan;
    [SerializeField] private WaterArrow _waterArrowrefab;
    [SerializeField] private WaterBall _waterBallPrefab;
    private EntityMover _mover;
    [SerializeField] private Transform _shotPos;
    [SerializeField] private float _fireAngle = 45f;

    public void Initialize(Entity entity)
    {
        _leviathan = entity as Leviathan;
    }

    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

    public void Attack()
    {
        _lastAtkTime = Time.time; ;
    }

    public void WaterArrowAttack()
    {
        _lastAtkTime = Time.time;
        Instantiate(_waterArrowrefab, _shotPos.position, Quaternion.identity);
    }

    public void WaterBallAttack()
    {
        _lastAtkTime = Time.time;

        // (1 / Cos¥È) * Mathf.Sqrt( (0.5f * g * distance^2) / (distance * Tan¥È + yOffset) );
        float angle = _fireAngle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angle);
        float tan = Mathf.Tan(angle);
        float gravity = Physics2D.gravity.magnitude;
        Vector2 direction = transform.position - _leviathan.target.transform.position;

        float distance = Mathf.Abs(direction.x);
        float yOffset = direction.y;

        float vZero = (1 / cos) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * tan + yOffset));

        float xDirection = -Mathf.Sign(direction.x);
        Vector2 velocity = new Vector3(xDirection * vZero * Mathf.Cos(angle), vZero * Mathf.Sin(angle));

        WaterBall waterBall = Instantiate(_waterBallPrefab, _shotPos.position, Quaternion.identity);

        waterBall.ThrowBullet(velocity, 4f);
    }
}
