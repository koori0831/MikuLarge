using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerHitState : EntityState
{
    protected Player _player;
    protected EntityMover _mover;
    public PlayerHitState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.GetComponent<Collider2D>().excludeLayers = _player.dashExclude;
        _player.HidingGun(false);
        _player.isHit = true;
    }

    public override void Exit()
    {
        _player.GetComponent<Collider2D>().excludeLayers = new LayerMask();
        _player.HidingGun(true);
        _player.isHit= false;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _player.ChangeState(StateName.Idle);
        }
    }
}
