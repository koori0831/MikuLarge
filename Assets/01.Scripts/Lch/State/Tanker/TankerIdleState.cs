using UnityEngine;

public class TankerIdleState : EntityState
{

    private Tanker _tanker;
    private EntityMover _mover;
    private float _count = 0;

    public TankerIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _tanker = entity as Tanker;
        _mover = _tanker.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
        _tanker.target = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public override void Update()
    {
        base.Update();
        FacingToPlayer();
        if (_tanker.CheckPlayerInRadius() && _tanker.AttackCompo.CanAttack()&&Manager.manager.RoomManager.DoorStatus && _count ==0)
        {
            _tanker.ChangeState(StateName.Attack);
            _count++;
        }
        if(_count > 0)
        {
            if(_tanker.CheckPlayerInRadius() && _tanker.AttackCompo.CanAttack())
            {
                _tanker.ChangeState(StateName.Attack);
            }
        }
    }


    private void FacingToPlayer()
    {
        float xDirection = _tanker.target.transform.position.x - _tanker.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }


}
