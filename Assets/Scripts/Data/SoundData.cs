using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Objects/SoundData")]
public class SoundData : ScriptableObject
{
    public AudioClip MenuMusic;
    public AudioClip GameplayMusic;
    public AudioClip EndGameMusic;
    public AudioClip ShipSfx;
    public AudioClip CrashSfx;
    public AudioClip ButtonClick;
}
