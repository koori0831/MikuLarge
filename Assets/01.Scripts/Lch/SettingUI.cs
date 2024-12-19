using UnityEngine;
using UnityEngine.UI;
using Ami.BroAudio;

public class SettingUI : MonoBehaviour
{

    [SerializeField] private Image _settingUiBackGround;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sFXSlider;

    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        BroAudio.SetVolume(BroAudioType.Music,Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume()
    {
        float volume = _musicSlider.value;
        BroAudio.SetVolume(BroAudioType.SFX, Mathf.Log10(volume) * 20);
    }

    public void Back()
   {
        _settingUiBackGround.gameObject.SetActive(false);
   }

    public void ExitGame()
    {
        Application.Quit();
    }
}
