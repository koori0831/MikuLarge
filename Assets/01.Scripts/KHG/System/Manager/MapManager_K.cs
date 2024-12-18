using System.Collections.Generic;
using UnityEngine;

public class MapManager_K : MonoBehaviour
{
    [Header("Setting")]
    public Vector3 _mapScale;
    public int _targetMapAmount; //º¸½º¸Ê Æ÷ÇÔ
    [Header("Map")]
    [SerializeField] private Transform _mapGrid;
    [SerializeField] private List<GameObject> _maps;
    [SerializeField] private GameObject _currentBossMap;

    private List<int> _spawnedMap = new List<int>();

    private void Start()
    {
        SetMap();
    }


    private void SetMap()
    {
        while (_maps != null)
        {
            int mapNum = Random.Range(0, _maps.Count);
            SpawnMap( _maps[mapNum] ,(_mapScale.x * _spawnedMap.Count) + _mapScale.x);
            _spawnedMap.Add(mapNum);
            if (_spawnedMap.Count >= _targetMapAmount - 1) break;
        }
        SpawnMap(_currentBossMap, (_mapScale.x * _spawnedMap.Count) + _mapScale.x);
    }


    private void SpawnMap(GameObject map,float xPos)
    {
        GameObject spawnedMap = Instantiate(map, _mapGrid);
        spawnedMap.transform.position = new Vector3(xPos, 0,0);
    }
}
