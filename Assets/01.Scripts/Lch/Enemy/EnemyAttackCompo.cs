using UnityEngine;
using Ami.BroAudio;

public class EnemyAttackCompo : MonoBehaviour, IEntityComponent
{

    [SerializeField] private float _cooldown;
    private float _lastAtkTime;
    private Enemy _enemys;

    public void Initialize(Entity entity)
    {
        _enemys = entity as Enemy;
    }

    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

    public void Attack()
    {
        _lastAtkTime = Time.time;
        BroAudio.Play(_enemys.AttakSound);
    }

   
}
