using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrruentGun", menuName = "SO/Player/CurrentGun")]
public class PlayerSavesSO : ScriptableObject
{
    public WeaponType nowWeaponType;
    public GameObject currentWeaponPrefab;
    public float CurrentHealth;
    public int NowCoin;
    public int CurrentAmmo;
}
