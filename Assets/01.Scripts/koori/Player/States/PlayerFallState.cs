using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    private const float FALL_SPEED_THRESHOLD = -20f;

    private float _maxDownVelocity;
    public PlayerFallState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    { }

    public override void Enter()
    {
        base.Enter();
        _maxDownVelocity = _mover.Velocity.y; //시작시 Y값 기록
        _mover.OnMoveVelocity += HandleVelocityChange;
    }

    private void HandleVelocityChange(Vector2 velocity)
    {
        //더 작은 값으로 갱신
        _maxDownVelocity = Mathf.Min(_maxDownVelocity, velocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (_mover.IsGrounded)
        {
            //착지순간에 측정하면 속도는 0이되버려.
            StateName nextState = _maxDownVelocity < FALL_SPEED_THRESHOLD ? StateName.Landing : StateName.Idle;
            _player.ChangeState(nextState);
        }
    }
    public override void Exit()
    {
        _mover.StopImmediately(false);
        base.Exit();
    }
}
