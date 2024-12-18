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
        Instantiate(_waterArrowrefab, _shotPos.position, Quaternion.identity);
    }

    public void WaterBallAttack()
    {
        for(int i = 0; i < 8; i++)
        {
            float RandY = Random.Range(-0.8f, 0.8f);
            Vector3 ShotPos = new Vector3(_shotPos.transform.position.x, _shotPos.transform.position.y + RandY);
            Instantiate(_waterBallPrefab, ShotPos , Quaternion.identity);
        }
    }
}
