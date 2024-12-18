using System;
using UnityEngine;
using UnityEngine.Events;

public class GrabItem : MonoBehaviour, IInteractable
{
    public UnityEvent Grab;
    public void Interact()
    {
        Grab?.Invoke();
    }
}
