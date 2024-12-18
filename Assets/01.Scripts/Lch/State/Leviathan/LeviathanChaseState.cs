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
        _phaseSelect = Random.Range(1, 3);
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
        _mover.SetMovement(targetDir.x);

        if (_leviathan.AttackCompo.CanAttack())
        {
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
                    Debug.Log("�̻��Ѱɷΰ�");
                    break;
            }
        }
    }
}