using UnityEngine;

public class GlabGun : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject gunPreFab;
    public void Interact(Player player)
    {
        player.Hands.PickUpGun(gunPreFab);
    }
}
