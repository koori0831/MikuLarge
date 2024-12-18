using System.Runtime.InteropServices;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private IInteractable _currentInteractable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            print(collision.name);
            _currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == _currentInteractable)
        {
            _currentInteractable = null;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }
}
