using UnityEngine;

public class PlayerAirState : EntityState
{
    protected Player _player;
    protected EntityMover _mover;

    protected PlayerAirState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.SetMoveSpeedMultiplier(0.7f);
    }

    public override void Update()
    {
        base.Update();
        float xInput = _player.PlayerInput.InputDirection.x;
        if (Mathf.Abs(xInput) > 0f)
        {
            _mover.SetMovement(xInput);
        }
    }

    public override void Exit()
    {
        _mover.SetMoveSpeedMultiplier(1f);
        base.Exit();
    }
}
