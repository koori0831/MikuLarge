using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWakeState : EntityState
{

    private Ghost _ghost;

    public GhostWakeState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _ghost = entity as Ghost;
    }

    public override void Enter()
    {
        base.Enter();
        _ghost.StartCoroutine(ChangeToChase());
    }

    private IEnumerator ChangeToChase()
    {
        yield return new WaitForSeconds(0.5f);
        _ghost.ChangeState(StateName.Move);
    }
}
