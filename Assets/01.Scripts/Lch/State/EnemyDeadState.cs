using UnityEngine;

public class EnemyDeadState : EntityState
{
    private Enemy _enemy;
    public EnemyDeadState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _enemy = entity as Enemy;
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            int DropCoinRange = Random.Range(0, 3);
            if (DropCoinRange == 1)
            {
                GameObject.Instantiate(_enemy.DropCoin, _enemy.transform.position, Quaternion.identity);
            }
            GameObject.Destroy(_enemy.gameObject);
        }
    }
}
