using UnityEngine;

public class MinimapUI : MonoBehaviour
{
    [SerializeField] private Manager _manager;
    [Header("Prefabs")]
    [SerializeField] private GameObject _minimapOutline;
    [SerializeField] private GameObject _minimapBoss;

    private void Start()
    {
        SetMinimap();
    }

    private void SetMinimap()
    {
        for(int i = 0; i < _manager.MapManager_K._targetMapAmount - 1; i++)
        {
            Instantiate(_minimapOutline, transform);
        }
        Instantiate(_minimapBoss, transform);
    }
}
