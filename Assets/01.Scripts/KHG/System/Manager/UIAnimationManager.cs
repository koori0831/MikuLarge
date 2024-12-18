using UnityEngine;

public class UIAnimationManager : MonoBehaviour
{
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
}