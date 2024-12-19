using UnityEngine;

public class PlayerMeleeState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    public PlayerMeleeState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately();
        _player.HidingGun(false);
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _player.ChangeState(StateName.Idle);
        }
    }

    public override void Exit()
    {
        _player.HidingGun(true);
        base.Exit();
    }
}
