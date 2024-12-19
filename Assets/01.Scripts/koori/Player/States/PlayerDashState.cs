using UnityEngine;

public class PlayerDashState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    private float _dashStartTime;
    private readonly int _deadLayer = LayerMask.NameToLayer("PlayerDead");
    private readonly int player = LayerMask.NameToLayer("Player");

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

        _player.gameObject.layer = _deadLayer;

        Vector2 speed = new Vector2(_renderer.FacingDirection * _player.dashSpeed, 0);
        _mover.AddForceToEntity(speed);

        _dashStartTime = Time.time;

        _player.GetComponent<Collider2D>().excludeLayers = _player.dashExclude;
        _player.HidingGun(false);
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
        _player.GetComponent<Collider2D>().excludeLayers = new LayerMask();
        _player.HidingGun(true);
        _player.gameObject.layer = player;
        base.Exit();
    }
}
