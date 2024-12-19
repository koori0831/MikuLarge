using UnityEngine;

public class PlayerAttackCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float _atkCooltime;
    [SerializeField] private StateSO _attackState;
    [SerializeField] private float _dashCooltime;
    [SerializeField] private StateSO _dashState;
    [SerializeField] private StateSO _jumpState;
    [SerializeField] private StateSO _fallState;
    [SerializeField] private PlayerDamageCast _damageCaster;
    private Player _player;
    private float _lastAtkTime;
    private float _lastDashTime;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _damageCaster.InitCaster(_player);
    }

    public bool AttemptDash()
    {
        if (_player.CurrentState == _player.GetState(_dashState)) return false;
        if (_player.CurrentState == _player.GetState(_jumpState)) return false;
        if (_player.CurrentState == _player.GetState(_fallState)) return false;
        if (_lastDashTime + _dashCooltime > Time.time) return false;

        _lastDashTime = Time.time;
        return true;
    }

    public bool AttemptAttack()
    {
        //이미 공격중이면 패스
        if (_player.CurrentState == _player.GetState(_attackState)) return false;
        if (_lastAtkTime + _atkCooltime > Time.time) return false;

        _lastAtkTime = Time.time;
        CastAttack();
        return true;
    }

    public bool AttemptShot()
    {
        if (_player.CurrentState == _player.GetState(_attackState)) return false;
        if (_player.CurrentState == _player.GetState(_dashState)) return false;

        return true;
    }

    private void CastAttack()
    {
        _damageCaster.CastDamage();
    }
}
