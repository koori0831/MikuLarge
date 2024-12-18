using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO")]
public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
{
    public event Action JumpEvent;
    public event Action InteractEvent;
    public event Action AttackEvent;
    public event Action DashEvent;
    public event Action ReloadEvent;
    public event Action ChangeWeaponEvent;
    public event Action NailEvent;
    public event Action MeleeEvent;
        
    public Vector2 InputDirection { get; private set; }

    public Controls Controls;
    private void OnEnable()
    {
        if(Controls == null)
        {
            Controls = new Controls();
            Controls.Player.SetCallbacks(this);
        }
        Controls.Player.Enable();
    }
        
    private void OnDisable()
    {
        Controls.Player.Disable();
    }
        
    public void OnMove(InputAction.CallbackContext context)
    {
        InputDirection = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed) 
            AttackEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed) 
            InteractEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed) 
            JumpEvent?.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.performed) 
            DashEvent?.Invoke();
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
            ReloadEvent?.Invoke();
    }

    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        if(context.performed)
            ChangeWeaponEvent?.Invoke();
    }

    public void OnNail(InputAction.CallbackContext context)
    {
        if(context.performed)
            NailEvent?.Invoke();
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        if(context.performed)
            MeleeEvent?.Invoke();
    }
}
