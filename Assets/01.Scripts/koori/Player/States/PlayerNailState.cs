using UnityEngine;

public class PlayerNailState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    public PlayerNailState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.Controls.Disable();
        _player.GetComponent<Collider2D>().excludeLayers = _player.dashExclude;
    }

    public override void Exit()
    {
        _player.isNailed = false;
        _player.GetComponent<Collider2D>().excludeLayers = new LayerMask();
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
