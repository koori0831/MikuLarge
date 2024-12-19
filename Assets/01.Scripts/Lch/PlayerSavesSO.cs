using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCrruentGun", menuName = "SO/Player/CurrentGun")]
public class PlayerSavesSO : ScriptableObject
{
    public WeaponType nowWeaponType;
    public Gun currentHandGun;
    public Gun currentHandsGun;
    public float CurrentHealth;
    public int NowCoin;
    public int CurrentAmon;
}
