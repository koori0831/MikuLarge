using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
        _mover.AddForceToEntity(new Vector2(0, _player.jumpPower));
        _mover.OnMoveVelocity += HandleVelocityChange;
    }

    public override void Exit()
    {
        _mover.OnMoveVelocity -= HandleVelocityChange;
        base.Exit();
    }

    private void HandleVelocityChange(Vector2 velocity)
    {
        if (velocity.y < 0)
        {
            _player.ChangeState(StateName.Fall);
        }

    }
}
