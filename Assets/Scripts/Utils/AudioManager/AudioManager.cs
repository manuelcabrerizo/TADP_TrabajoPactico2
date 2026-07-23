using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IService
{
    public bool IsPersistance => true;
    public SoundData Sounds;
    public AudioSource AudioSourcePrefab;
    public AudioSource MusicAudioSource => musicAudioSource;
    public Stack<AudioSource> SfxAudioSources => sfxAudioSources;

    private AudioSource musicAudioSource;
    private Stack<AudioSource> sfxAudioSources;


    private void Awake()
    {
        if (ServiceProvider.Instance.ContainsService<AudioManager>())
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        ServiceProvider.Instance.AddService<AudioManager>(this);
        sfxAudioSources = new Stack<AudioSource>();
        musicAudioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public void StopMusic()
    {
        if (musicAudioSource)
        {
            musicAudioSource.Stop();
        }
    }
    
    public void PlayClip(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        AudioSource audioSource = GetAudioSource();
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(ReleaseAudioSourceIfFinish(audioSource));
    }

    private AudioSource GetAudioSource()
    {
        AudioSource audioSource = null;
        if(sfxAudioSources.Count == 0)
        {
            audioSource = Instantiate(AudioSourcePrefab, transform);
        }
        else
        {
            audioSource = sfxAudioSources.Pop();
        }
        return audioSource;
    }

    private IEnumerator ReleaseAudioSourceIfFinish(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.Stop();
        audioSource.clip = null;
        sfxAudioSources.Push(audioSource);
    }
}
