using UnityEngine;
using UnityEngine.Rendering;

public class MusicSettings : MonoBehaviour
{
    public  void ChangeIsPlaying(bool value)
    {
        PlayerSettings.isPlayable = value;
    }

    public void ChangeVolume(float volumeValue)
    {
        PlayerSettings.volume = volumeValue;
    }
}
