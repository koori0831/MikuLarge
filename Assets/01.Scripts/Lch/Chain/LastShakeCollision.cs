using UnityEngine;

public class LastShakeCollision : MonoBehaviour
{
    private void OnEnable()
    {
        Camera mainCamera = Camera.main;

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        boxCollider.size = new Vector2(cameraWidth, cameraHeight);

        transform.position = mainCamera.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInParent<LastShake>().ChildrenCollisionEnter(collision);
    }
}
