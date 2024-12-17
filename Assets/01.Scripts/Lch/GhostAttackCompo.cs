using UnityEngine;

public class GhostAttackCompo : MonoBehaviour, IEntityComponent
{

    [SerializeField] private float _cooldown;
    private float _lastAtkTime;
    private Ghost _ghost;

    public void Initialize(Entity entity)
    {
        _ghost = entity as Ghost;
    }

    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;
}
