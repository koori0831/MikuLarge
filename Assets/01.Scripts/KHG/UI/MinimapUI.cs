using UnityEngine;

public class MinimapUI : MonoBehaviour
{
    [SerializeField] private Manager _manager;
    [SerializeField] private Transform _currentMapIcon;
    [Header("Prefabs")]
    [SerializeField] private GameObject _minimapOutline;
    [SerializeField] private GameObject _minimapBoss;

    private void Start()
    {
        SetMinimap();
        SetMinimapPosistion(0);
    }

    private void SetMinimap()
    {
        for(int i = 0; i < _manager.MapManager_K._targetMapAmount; i++)
        {
            Instantiate(_minimapOutline, transform);                        
        }
        Instantiate(_minimapBoss, transform);
    }


    public void SetMinimapPosistion(int num)
    {
        _currentMapIcon.parent = transform.GetChild(num);
        _currentMapIcon.position = transform.GetChild(num).position;
    }
}
