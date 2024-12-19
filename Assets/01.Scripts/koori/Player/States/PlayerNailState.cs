using UnityEngine;

public class PlayerNailState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    private readonly int _deadLayer = LayerMask.NameToLayer("PlayerDead");
    private readonly int player = LayerMask.NameToLayer("Player");
    public PlayerNailState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.Controls.Disable();
        _mover.StopImmediately();
        _player.gameObject.layer = _deadLayer;
        _mover.enabled = false;
        _player.GetComponent<Collider2D>().excludeLayers = _player.dashExclude;
    }

    public override void Exit()
    {
        _player.isNailed = false;
        _mover.enabled = true;
        _player.GetComponent<Collider2D>().excludeLayers = new LayerMask();
        _player.gameObject.layer = player;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (_player.isNailed)
        {
            _player.ChangeState(StateName.Idle);
        }
    }
}
