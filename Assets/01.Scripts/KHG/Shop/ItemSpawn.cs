using UnityEngine;
using System.Collections.Generic;
public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private Transform _itemParent;
    [Header("Prefabs")]
    [SerializeField] private GameObject[] _item;

    private List<int> _spawnList = new List<int>();

    private void OnEnable()
    {
        foreach (Transform spawner in _spawnPoint)
        {
            int spawnIndex = Random.Range(0, _item.Length);
            while (true)
            {
                if (_spawnList.Contains(spawnIndex) == false) break; ; 
                spawnIndex = Random.Range(0, _item.Length);
            }
            
            GameObject item = Instantiate(_item[spawnIndex], _itemParent);

            item.transform.position = spawner.transform.position;
            _spawnList.Add(spawnIndex);
            
        }
    }
}
