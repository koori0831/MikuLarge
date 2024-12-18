using UnityEngine;

public class EnemyHitState : EntityState
{

    private Enemy _enemy;
    private EntityRenderer _renderer;

    public EnemyHitState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _enemy = entity as Enemy;
        _renderer = _enemy.GetCompo<EntityRenderer>();
    }

    public override void Enter()
    {
        base.Enter();
        Vector3 dir = _enemy.target.transform.position - _enemy.transform.position;
        _renderer.FlipController(dir.x);
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _enemy.ChangeState(StateName.Idle);
        }
    }
}
