using UnityEngine;

public class PlayerDeadState : EntityState
{
    protected Player _player;
    protected EntityMover _mover;
    public PlayerDeadState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.GetComponent<Collider2D>().excludeLayers = _player.dashExclude;
        _player.HidingGun(false);
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _player.IsDead = true;
            Debug.Log("게이 오버");
        }
    }
}
