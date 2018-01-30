using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class CreditsUI : MonoBehaviour {

	[Inject] private PlayMusicClipSignal _playMusicClipSignal;
	[SerializeField] private AudioClip _backButtonSound;
	
	[Inject]
	private GameStartSignal _gameStartSignal;

	public void StartGame()
	{
		_playMusicClipSignal.Fire(_backButtonSound);	
		_gameStartSignal.Fire();
	}
}
