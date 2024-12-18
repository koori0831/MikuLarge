using UnityEngine;

public class AsmodeusAttackCompo : MonoBehaviour,IEntityComponent
{
    [SerializeField] private float _cooldown;
    private float _lastAtkTime;
    private ADEnemy _adEnemys;
    [SerializeField] private Charm _charmPrefab;

    public void Initialize(Entity entity)
    {
        _adEnemys = entity as ADEnemy;
    }

    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

    public void CharmAttack()
    {
        _lastAtkTime = Time.time;
        Instantiate(_charmPrefab, transform.position, Quaternion.identity);
    }

    public void DarkAttack()
    {

    }

}
