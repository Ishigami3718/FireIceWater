using UnityEngine;
using UnityEngine.Rendering;

public class MusicSettings : MonoBehaviour
{
    public  void ChangeIsPlaying(bool value)
    {
        PlayerSettings.isPlayable = !PlayerSettings.isPlayable;
    }

    public void ChangeVolume(float volumeValue)
    {
        PlayerSettings.volume = volumeValue;
    }
}
