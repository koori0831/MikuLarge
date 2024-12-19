using UnityEngine;

public class EnemyHitState : EntityState
{

    private Enemy _enemy;
    private int _soulDropRnage;
    public EnemyHitState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _enemy = entity as Enemy;
        _renderer = _enemy.GetCompo<EntityRenderer>();
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.target = GameObject.FindWithTag("Player").GetComponent<Player>();
        Vector3 dir = _enemy.target.transform.position - _enemy.transform.position;
        _renderer.FlipController(dir.x);
        _soulDropRnage = Random.Range(1, 6);
    }

    public override void Update()
    {
        base.Update();
        if(_soulDropRnage <= 2)
        {
            GameObject.Instantiate(_enemy.SoulPrefab, _enemy.transform.position, Quaternion.identity);
        }
        if (_isTriggerCall)
        {
            _enemy.ChangeState(StateName.Idle);
        }
    }
}
