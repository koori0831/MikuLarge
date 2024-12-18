using UnityEngine;

public class AsmodeusAttackCompo : MonoBehaviour,IEntityComponent
{
    [SerializeField] private float _cooldown;
    private float _lastAtkTime;
    private Asmodeus _asmodeus;
    [SerializeField] private Charm _charmPrefab;
    [SerializeField] private Dark _darkPrefab;
    private EntityMover _mover;

    public void Initialize(Entity entity)
    {
        _asmodeus = entity as Asmodeus;
    }

    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

    public void DashAttack()
    {
        _lastAtkTime = Time.time;
        Vector2 targetDir = new Vector2(_asmodeus.target.transform.position.x - 2.5f, _asmodeus.target.transform.position.y);
        _mover = _asmodeus.GetCompo<EntityMover>();
        _mover.AddForceToEntity(targetDir);
    }

    public void CharmAttack()
    {
        _lastAtkTime = Time.time;
        Instantiate(_charmPrefab, transform.position, Quaternion.identity);
    }

    public void DarkAttack()
    {
        _lastAtkTime = Time.time;
        Instantiate(_darkPrefab, transform.position, Quaternion.identity);
    }

}
