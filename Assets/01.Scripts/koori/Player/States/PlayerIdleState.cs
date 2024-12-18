using UnityEngine;

public class PlayerIdleState : EntityState
{
    private EntityMover _mover;
    private Player _player;

    public PlayerIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(false);
    }

    public override void Update()
    {
        base.Update();
        float xInput = _player.PlayerInput.InputDirection.x;

        if (Mathf.Abs(xInput) > 0)
            _player.ChangeState(StateName.Move);
    }
}
