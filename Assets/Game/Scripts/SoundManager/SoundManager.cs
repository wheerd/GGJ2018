using UnityEngine;
using Zenject;

public class SoundManager : MonoBehaviour
{
    [Inject]
    PlayMusicStringSignal _playMusicStringSignal;
    
    [Inject]
    PlayMusicClipSignal _playMusicClipSignal;

    // Use this for initialization
    void Awake()
    {
        _playMusicStringSignal += PlayMusic;
        _playMusicClipSignal += PlayMusic;
    }

    void OnDestroy()
    {
        _playMusicStringSignal -= PlayMusic;
        _playMusicClipSignal -= PlayMusic;
    }

    void PlayMusic(AudioClip clip)
    {
        DoubleAudioSource player = GetComponent<DoubleAudioSource>();
        player.CrossFade(clip, 1.0f, 2.0f);   
    }
    
    void PlayMusic(string soundFile)
    {
        AudioClip clip = Resources.Load<AudioClip>(soundFile);
        PlayMusic(clip);
    }
}
