using UnityEngine;

public class ADIdleState : EntityState
{

    private ADEnemy _adEnemy;
    private EntityMover _mover;


    public ADIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _adEnemy = entity as ADEnemy;
        _mover = _adEnemy.GetCompo<EntityMover>();
        
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
        _adEnemy.target = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public override void Update()
    {
        base.Update();
        FacingToPlayer();
        if (_adEnemy.CheckAttackToPlayerRadius() && _adEnemy.AttackCompo.CanAttack() 
            && _adEnemy.CheckPlayerInRadius() && Manager.manager.RoomManager.DoorStatus)
        {
            _adEnemy.ChangeState(StateName.Attack);
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _adEnemy.target.transform.position.x - _adEnemy.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
