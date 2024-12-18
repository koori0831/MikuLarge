using UnityEngine;

public class EnemyAttackCompo : MonoBehaviour, IEntityComponent
{

    [SerializeField] private float _cooldown;
    private float _lastAtkTime;
    private Entity _enemys;

    public void Initialize(Entity entity)
    {
        _enemys = entity;
    }

    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

   
}
