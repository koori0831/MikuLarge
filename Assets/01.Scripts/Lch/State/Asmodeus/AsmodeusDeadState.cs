using UnityEngine;

public class AsmodeusDeadState : EntityState
{
    private Asmodeus _asmodeus;
    private int _spawnItemCount;
    private readonly int _deadLayer = LayerMask.NameToLayer("DeadBoss");
    public AsmodeusDeadState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _asmodeus = entity as Asmodeus;
    }

    public override void Enter()
    {
        base.Enter();
        _spawnItemCount = Random.Range(0, _asmodeus.ItemList.DropItemList.Count);
        _asmodeus.gameObject.layer = _deadLayer;
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            GameObject.Instantiate(_asmodeus.ItemList.DropItemList[_spawnItemCount], _asmodeus.DropPos.position, Quaternion.identity);
            _asmodeus.BossDeadEvnet.RaiseEvent();
            GameObject.Destroy(_asmodeus.gameObject);
        }
    }
}
