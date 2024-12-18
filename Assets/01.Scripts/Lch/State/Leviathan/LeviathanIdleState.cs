using UnityEngine;

public class LeviathanIdleState : EntityState
{

    private Leviathan _leviathan;
    private EntityMover _mover;

    public LeviathanIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _leviathan = entity as Leviathan;
        _mover = _leviathan.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _leviathan.target = GameObject.FindWithTag("Player").GetComponent<Player>();
        _mover.StopImmediately(true);
        FacingToPlayer();
    }

    public override void Update()
    {
        base.Update();
        if (_leviathan.CheckPlayerInRadius())
        {
            _leviathan.ChangeState(StateName.Move);
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _leviathan.target.transform.position.x - _leviathan.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }


}
