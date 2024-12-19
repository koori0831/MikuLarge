using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager manager { get; private set; }

    public MapManager_K MapManager_K;
    public CameraManager_K CameraManager_K;
    public ResourceManager ResourceManager;
    public RoomManager RoomManager;

    [Header("UI")]
    public ResourceUI ResourceUI;
    public MinimapUI MinimapUI;
    public UIAnimationManager AnimationManager;
    public UIManager UIManager;



    private void Awake()
    {
        if (manager == null) manager = this;
        else Destroy(this);
    }

    public void DestoryManager()
    {
        Destroy(gameObject);
    }
}