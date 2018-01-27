using UnityEngine;
using Zenject;

public class PlayMusicStringSignal : Signal<PlayMusicStringSignal, string>
{
}

public class PlayMusicClipSignal : Signal<PlayMusicClipSignal, AudioClip>
{
}
