using UnityEngine;

public class UIAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animatorBackground;
    [SerializeField] private Animator _animatorUI;
    public void PlayerAnim(string animName)
    {
        if(_animatorBackground.GetBool(animName)) _animatorBackground.Play(animName);
        else _animatorUI.Play(animName);
    }
}