using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager manager { get; private set; }

    public MapManager_K MapManager_K;
    public CameraManager_K CameraManager_K;
    public ResourceManager ResourceManager;

    [Header("UI")]
    public ResourceUI ResourceUI;
    public MinimapUI MinimapUI;
    public UIAnimationManager AnimationManager;



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (manager == null) manager = this;
        else Destroy(this);
    }
}
