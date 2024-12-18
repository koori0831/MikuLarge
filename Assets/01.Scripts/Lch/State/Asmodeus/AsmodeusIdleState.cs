using UnityEngine;
using System.Collections;

public class AsmodeusIdleState : EntityState
{

    private Asmodeus _asmodeus;
    private EntityMover _mover;
    private int _phaseSelect = 0;

    public AsmodeusIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _asmodeus = entity as Asmodeus;
        _mover = _asmodeus.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
        _phaseSelect = Random.Range(1, 3);
        _asmodeus.target = GameObject.FindWithTag("Player").GetComponent<Player>();
        FacingToPlayer();
    }

    public override void Update()
    {
        base.Update();
        if (_asmodeus.AttackCompo.CanAttack())
        {
            switch (_phaseSelect)
            {
                case 1:
                    _asmodeus.ChangeState(StateName.Phase1);
                    break;
                case 2:
                    _asmodeus.ChangeState(StateName.Phase2);
                    break;
                case 3:
                    _asmodeus.ChangeState(StateName.Phase3);
                    break;
                default:
                    Debug.Log("이상한걸로감");
                    break;
            }
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _asmodeus.target.transform.position.x - _asmodeus.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}
