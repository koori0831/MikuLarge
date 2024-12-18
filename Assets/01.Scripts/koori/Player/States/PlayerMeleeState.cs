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
    }
}