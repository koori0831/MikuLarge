using System;
using System.Collections;
using UnityEngine;

public class EntityHealth : MonoBehaviour, IEntityComponent, IDamageable
{
    [field:SerializeField] public float MaxHealth = 50f;
    public float _currentHealth;
    [SerializeField] private float _knockBackTime = 0.5f;
    private Entity _entity;
    private EntityMover _mover;

    public event Action<Entity> OnHit;
    public event Action OnDeath;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
        _mover = _entity.GetCompo<EntityMover>();
        
        _currentHealth = MaxHealth;
    }


    public void ApplyDamage(float damage, Vector2 direction, Vector2 knockBack, Entity dealer)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, MaxHealth);
        StartCoroutine(ApplyKnockBack(knockBack));
        OnHit?.Invoke(dealer);

        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    private IEnumerator ApplyKnockBack(Vector2 knockBack)
    {
        _mover.CanManualMove = false;
        _mover.StopImmediately(true);
        _mover.AddForceToEntity(knockBack);
        yield return new WaitForSeconds(_knockBackTime);
        _mover.CanManualMove = true;
    }
}
