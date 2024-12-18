using System;
using UnityEngine;

public class EntityAnimator : MonoBehaviour, IEntityComponent
{
    public event Action OnAnimationEnd;
    public event Action OnAttackEvent;
    protected Entity _entity;
    
    protected virtual void AnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }

    protected virtual void AttackEvent()
    {
        OnAttackEvent?.Invoke();
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;    
    }
}
