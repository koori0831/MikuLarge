using UnityEngine;

public class LeviathanChaseState : EntityState
{

    private Leviathan _leviathan;
    private EntityMover _mover;
    private int _phaseSelect = 0;

    public LeviathanChaseState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _leviathan = entity as Leviathan;
        _mover = _leviathan.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(false);
    }

    public override void Update()
    {
        base.Update();
        Vector2 targetDir = _leviathan.target.transform.position - _leviathan.transform.position;
        _mover.SetMovement(targetDir.normalized.x);

        FacingToPlayer();

        if (_leviathan.AttackCompo.CanAttack())
        {
            _phaseSelect = Random.Range(1, 4);
            switch (_phaseSelect)
            {
                case 1:
                    _leviathan.ChangeState(StateName.Phase1);
                    break;
                case 2:
                    _leviathan.ChangeState(StateName.Phase2);
                    break;
                case 3:
                    _leviathan.ChangeState(StateName.Phase3);
                    break;
                default:
                    Debug.Log("이상한걸로감");
                    break;
            }
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _leviathan.target.transform.position.x - _leviathan.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
