using UnityEngine;

public class LeviathenDeadState : EntityState
{

    private Leviathan _leviathan;
    private readonly int _deadLayer = LayerMask.NameToLayer("DeadBoss");
    private SpriteRenderer _spriteRenderer;
    private EntityMover _mover;
    public LeviathenDeadState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _leviathan = entity as Leviathan;
        _spriteRenderer = _leviathan.GetComponentInChildren<SpriteRenderer>();
        _mover = entity.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _leviathan.gameObject.layer = _deadLayer;
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _renderer.enabled = false;
            _mover.enabled = false;
            _spriteRenderer.color = Color.gray;
            _leviathan._bossDeadEvnet.RaiseEvent(true);
        }
    }
}
