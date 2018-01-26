using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class HighscoreUI : MonoBehaviour {

	[Inject]
	private GameStartSignal _gameStartSignal;

	public void StartGame()
	{
		_gameStartSignal.Fire();
	}
}
