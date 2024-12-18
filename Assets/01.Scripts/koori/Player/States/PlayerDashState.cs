using UnityEngine;

public class PlayerDashState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    private float _dashStartTime;

    public PlayerDashState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
        _mover.SetGravityScale(0);
        _mover.CanManualMove = false;

        Vector2 speed = new Vector2(_renderer.FacingDirection * _player.dashSpeed, 0);
        _mover.AddForceToEntity(speed);
        //원래는 거리 측정하고 레이캐스트 쏴야해. 근데 지금은 간략하게. 중급이니까.
        _dashStartTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (_dashStartTime + _player.dashDuration < Time.time)
        {
            _player.ChangeState(StateName.Idle);
        }
    }

    public override void Exit()
    {
        _mover.StopImmediately(true);
        _mover.CanManualMove = true;
        _mover.SetGravityScale(1f);
        base.Exit();
    }
}
