using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DropItemListSO", menuName = "SO/Item/DropItemListSO")]
public class DropItemListSO : ScriptableObject
{
    public List<GameObject> DropItemList;
}
