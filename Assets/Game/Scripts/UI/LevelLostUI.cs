using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class LevelLostUI : MonoBehaviour {

	[Inject]
	private GameStartSignal _gameStartSignal;
	
	[Inject]
	private GameHighscoreSignal _gameHighscoreSignal;

	public void StartGame()
	{
		_gameStartSignal.Fire();
	}
	
	public void ShowHighscore()
	{
		_gameHighscoreSignal.Fire();
	}
}
