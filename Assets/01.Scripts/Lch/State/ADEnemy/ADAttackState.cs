using UnityEngine;

public class ADAttackState : EntityState
{

    private ADEnemy _adEnemy;
    private EntityRenderer _renderer;
    private Vector2 _targetDir;

    public ADAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _adEnemy = entity as ADEnemy;
        _renderer = _adEnemy.GetCompo<EntityRenderer>();
    }

    public override void Enter()
    {
        base.Enter();
        FacingToPlayer();
    }

    public override void Update()
    {
        base.Update();
        _renderer.FlipController(_targetDir.x);
        if (_isTriggerCall)
        {
            _adEnemy.ChangeState(StateName.Idle);
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _adEnemy.target.transform.position.x - _adEnemy.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
