using System;
using UnityEngine;

public class EntityAnimator : MonoBehaviour, IEntityComponent
{
    public event Action OnAnimationEnd;
    protected Entity _entity;
    
    protected virtual void AnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;    
    }
}
