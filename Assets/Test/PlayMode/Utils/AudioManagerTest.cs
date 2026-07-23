using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class AudioManagerTest
{
    private AudioManager AudioManager => ServiceProvider.Instance.GetService<AudioManager>();

    private AudioSource audioSourcePrefab;
    private SoundData soundData;
    private GameObject gameObject;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        GameObject listener = new GameObject("AudioListener");
        listener.AddComponent<AudioListener>();

        GameObject prefab = new GameObject("AudioSourcePrefab");
        audioSourcePrefab = prefab.AddComponent<AudioSource>();

        soundData = ScriptableObject.CreateInstance<SoundData>();
        soundData.MenuMusic = AudioClip.Create("TestClip", 44100, 1, 44100, false);
        soundData.GameplayMusic = AudioClip.Create("TestClip", 44100, 1, 44100, false);
        soundData.EndGameMusic = AudioClip.Create("TestClip", 44100, 1, 44100, false);
        soundData.ShipSfx = AudioClip.Create("TestClip", 44100, 1, 44100, false);
        soundData.CrashSfx = AudioClip.Create("TestClip", 44100, 1, 44100, false);
    }

    [UnitySetUp]
    public IEnumerator Setup()
    {
        gameObject = new GameObject("AudioManager");
        gameObject.AddComponent<AudioSource>();
        AudioManager audioManager = gameObject.AddComponent<AudioManager>();
        audioManager.AudioSourcePrefab = audioSourcePrefab;
        audioManager.Sounds = soundData;
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        ServiceProvider.Instance.ClearAllServices();
        Object.Destroy(gameObject);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayMusic_CorrectlySetAndPlayTheAudioSource()
    {
        AudioManager.PlayMusic(AudioManager.Sounds.GameplayMusic);
        yield return new WaitForSeconds(0.2f);
        Assert.AreEqual(AudioManager.Sounds.GameplayMusic, AudioManager.MusicAudioSource.clip);
        Assert.IsTrue(AudioManager.MusicAudioSource.isPlaying);
    }

    [UnityTest]
    public IEnumerator StopMusic_CorrectlyStopTheAudioSource()
    {
        AudioManager.StopMusic();
        yield return new WaitForSeconds(0.2f);
        Assert.IsFalse(AudioManager.MusicAudioSource.isPlaying);
    }

    [UnityTest]
    public IEnumerator PlayClip_ReleaseClipIfFinish()
    {
        AudioManager.PlayClip(AudioManager.Sounds.ShipSfx);
        Assert.AreEqual(0, AudioManager.SfxAudioSources.Count);
        yield return new WaitForSeconds(2.0f);
        Assert.AreEqual(1, AudioManager.SfxAudioSources.Count);
    }

    [UnityTest]
    public IEnumerator PlayClip_UseFreeClipIfPosible()
    {
        AudioManager.PlayClip(AudioManager.Sounds.ShipSfx);
        yield return new WaitForSeconds(2.0f);
        Assert.AreEqual(1, AudioManager.SfxAudioSources.Count);
        AudioManager.PlayClip(AudioManager.Sounds.ShipSfx);
        Assert.AreEqual(0, AudioManager.SfxAudioSources.Count);
    }
}