using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Vector3 _mapScale;
    [SerializeField] private int _targetMapAmount;
    [Header("Map")]
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
            SpawnMap( _maps[mapNum] ,_mapScale.x * _spawnedMap.Count);
            _spawnedMap.Add(mapNum);
            if (_spawnedMap.Count >= _targetMapAmount) break;
        }
        SpawnMap(_currentBossMap, _mapScale.x * _spawnedMap.Count);
    }


    private void SpawnMap(GameObject map,float xPos)
    {
        GameObject spawnedMap = Instantiate(map);
        spawnedMap.transform.position = new Vector3(xPos, 0,0);
    }
}
