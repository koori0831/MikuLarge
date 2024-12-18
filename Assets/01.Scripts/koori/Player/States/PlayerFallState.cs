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
        _maxDownVelocity = _mover.Velocity.y; //���۽� Y�� ���
        _mover.OnMoveVelocity += HandleVelocityChange;
    }

    private void HandleVelocityChange(Vector2 velocity)
    {
        //�� ���� ������ ����
        _maxDownVelocity = Mathf.Min(_maxDownVelocity, velocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (_mover.IsGrounded)
        {
            //���������� �����ϸ� �ӵ��� 0�̵ǹ���.
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
