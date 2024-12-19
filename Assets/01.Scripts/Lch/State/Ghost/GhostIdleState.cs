using UnityEngine;

public class GhostIdleState : EntityState
{
    private Ghost _ghost;
    private EntityMover _mover;
    public GhostIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _ghost = entity as Ghost;
        _mover = _ghost.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
    }
    public override void Update()
    {
        base.Update();
        if (_ghost.CheckPlayerInRadius() && Manager.manager.RoomManager.DoorStatus)
        {
            _ghost.ChangeState(StateName.Wake);
        }
        
    }
}
