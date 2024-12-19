using UnityEngine;

public class EnemyDeadState : EntityState
{
    private Enemy _enemy;
    private readonly int _deadLayer = LayerMask.NameToLayer("DeadBoss");
    public EnemyDeadState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _enemy = entity as Enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.gameObject.layer = _deadLayer;
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _enemy.IsDead = true;
            int DropCoinRange = Random.Range(0, 3);
            if (DropCoinRange == 1)
            {
                GameObject.Instantiate(_enemy.DropCoin, _enemy.transform.position, Quaternion.identity);
            }
            GameObject.Destroy(_enemy.gameObject);
        }
    }
}
