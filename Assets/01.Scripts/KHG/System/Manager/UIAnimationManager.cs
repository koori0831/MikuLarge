using System.Collections;
using UnityEngine;

public class UIAnimationManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private Animator _animatorBackground;
    [SerializeField] private Animator _animatorUI;
    [SerializeField] private Animator _animatorStagepass;
    public void PlayerAnim(string animName)
    {
        _animatorBackground.Play(animName);
        _animatorUI.Play(animName);
        _animatorStagepass.Play(animName);
        //if(_animatorBackground.GetBool(animName)) _animatorBackground.Play(animName);
        //else _animatorUI.Play(animName);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            FocusAnim();
        }
    }


    public void FocusAnim()
    {
        _player.PlayerInput.Controls.Player.Disable();
        Manager.manager.CameraManager_K.SetCameraAim(4.2f);
        _animatorBackground.Play("Background_Focus");
        _animatorUI.Play("UI_Focus");
    }
    private IEnumerator Resume(float time)
    {
        yield return new WaitForSeconds(time);
        _player.PlayerInput.Controls.Player.Disable();
    }
}