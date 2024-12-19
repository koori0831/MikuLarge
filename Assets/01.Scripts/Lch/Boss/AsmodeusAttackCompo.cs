using UnityEngine;
using Ami.BroAudio;

public class AsmodeusAttackCompo : MonoBehaviour,IEntityComponent
{
    [SerializeField] private float _cooldown;
    private float _lastAtkTime;
    private Asmodeus _asmodeus;
    [SerializeField] private Charm _charmPrefab;
    [SerializeField] private Dark _darkPrefab;
    private EntityMover _mover;
    [SerializeField] private Transform _shotPos;

    public void Initialize(Entity entity)
    {
        _asmodeus = entity as Asmodeus;
    }

    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

    public void DashAttack()
    {
        _lastAtkTime = Time.time;;
        BroAudio.Play(_asmodeus.Phase1Sound);
    }

    public void CharmAttack()
    {
        _lastAtkTime = Time.time;
        Instantiate(_charmPrefab, _shotPos.position, Quaternion.identity);
        BroAudio.Play(_asmodeus.Phase2Sound);

    }

    public void DarkAttack()
    {
        _lastAtkTime = Time.time;
        Instantiate(_darkPrefab, _shotPos.position, Quaternion.identity);
        BroAudio.Play(_asmodeus.Phase3Sound);

    }

}
