using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Transform firePos;
    public WeaponType type;
    public virtual void Shot()
    {
        Debug.Log("»§¾ß");
    }
}
