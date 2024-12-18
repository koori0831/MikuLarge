using UnityEngine;

public class EnemyDeadState : EntityState
{
    public EnemyDeadState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            GameObject.Destroy(_entity.gameObject);
        }
    }
}
