using UnityEngine;

public class ClipsData : IService
{
    public bool IsPersistance => false;

    public AudioClip[] Clips { get; private set; } = null;

    public ClipsData(AudioClip[] clips)
    {
        Clips = clips;
    }
}
