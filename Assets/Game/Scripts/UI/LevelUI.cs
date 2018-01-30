using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class LevelUI : MonoBehaviour {

	[Inject] private PlayMusicClipSignal _playMusicClipSignal;
	[SerializeField] private AudioClip _levelStartSound;

	
	[Inject]
	private LevelWinSignal _levelWinSignal;
	
	[Inject]
	private LevelLostSignal _levelLostSignal;
	
	[Inject] private PauseSignal _pauseSignal;

	void Start()
	{
		_playMusicClipSignal.Fire(_levelStartSound);

	}
	
	public void Pause()
	{
		_pauseSignal.Fire();
	}

	public void ShowWin()
	{
		_levelWinSignal.Fire();
	}

	public void ShowLost()
	{
		_levelLostSignal.Fire();
	}
}
