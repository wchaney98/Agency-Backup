using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;

#region Enum
/// <summary>
/// An enumeration that holds every sound effect in the game.
/// </summary>
public enum SoundFile
{
    //Music
    LevelTrack1,
    LevelTrack2,
    MenuTrack1,

    //delet
    Steve0,

    HumanDeath0,
    HumanDeath1,
    HumanDeath2,

    ArmorHit0,
    FleshHit0,
    FleshHit1,

    PistolShot0,
    PistolShot1,

    SMG0,
    SMG1,

    BulletWhizz0,
    BulletWhizz1,
    BulletWhizz2,

    HeavyShot0,
    Shotgun0,

    ExplosionMedium,
    Flashbang0,

    Laser0,

    TurretDeploy,

    MeleeEnemySeek
}
# endregion

public class SoundManager : SingletonBehavior<SoundManager>
{
    const string AUDIO_FILE_LOCATION = "Audio";

    float volume = 1f;

    private string currScene = "";

    /// <summary>
    /// A collection of all of the sound effects in the game.
    /// </summary>
    private Dictionary<SoundFile, AudioClip> SoundEffects
    { get; set; }

    /// <summary>
    /// Source for the sound effects
    /// </summary>
    private static AudioSource SoundEffectSource
    { get; set; }

    /// <summary>
    /// Source for the BGM
    /// </summary>
    private static AudioSource BGMSource
    { get; set; }

    protected override void Init()
    {
        SoundEffects = new Dictionary<SoundFile, AudioClip>();

        //Create a temporary dictionary that loads all of the Audio files from a specific location.
        //Key = name of file, Value = file itself.
        Dictionary<string, AudioClip> clips = Resources.LoadAll<AudioClip>(AUDIO_FILE_LOCATION).ToDictionary(t => t.name);

        //Iterates through the loaded sound files and adds them to the Enum to AudioClip dictionary.
        foreach (KeyValuePair<string, AudioClip> c in clips)
        {
            SoundEffects.Add((SoundFile)Enum.Parse(typeof(SoundFile), c.Key, true), c.Value);
        }

        //Creates a single sound effect source. Can play every sound in the game through this unless you want to have different effects
        //such as different pitch/volume for different sources.
        //TODO: each object have its own audio source
        if (SoundEffectSource == null)
        {
            Debug.Log("Creating SoundEffectSource");
            SoundEffectSource = new GameObject("SoundEffectSource", typeof(AudioSource)).GetComponent<AudioSource>();
            SoundEffectSource.volume = 1.3f;
            DontDestroyOnLoad(SoundEffectSource.gameObject);
        }

        //Creates a single background music source.
        if (BGMSource == null)
        {
            Debug.Log("Creating BGMSource");
            BGMSource = new GameObject("BGMSource", typeof(AudioSource)).GetComponent<AudioSource>();
            BGMSource.volume = .22f;
            BGMSource.loop = true;
            DontDestroyOnLoad(BGMSource.gameObject);
        }
    }

    /// <summary>
    /// Updates the audio manager
    /// </summary>
    public void Update()
    {
        if (currScene != SceneManager.GetActiveScene().name)
        {
            currScene = SceneManager.GetActiveScene().name;
            if (currScene != "MainGame")
            {
                ChangeBGM(SoundFile.MenuTrack1);
            }
            else
            {
                if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
                    ChangeBGM(SoundFile.LevelTrack1);
                else
                    ChangeBGM(SoundFile.LevelTrack2);
            }
        }
    }

    /// <summary>
    /// Plays a single sound effect
    /// </summary>
    /// <param name="sound">The sound effect we want to play</param>
    public void DoPlayOneShot(SoundFile[] sounds, Vector3? location = null, float volumeScale = 1)
    {
        if (location == null)
            location = Vector3.zero;
        AudioSource.PlayClipAtPoint(SoundEffects[sounds[UnityEngine.Random.Range(0, sounds.Length)]], (Vector3)location, volumeScale * volume);
    }

    /// <summary>
    /// Changes the BGM to something else
    /// </summary>
    /// <param name="sound">The bgm sound we want to play</param>
    public void ChangeBGM(SoundFile sound)
    {
        BGMSource.clip = SoundEffects[sound];
        BGMSource.Play();
    }
}