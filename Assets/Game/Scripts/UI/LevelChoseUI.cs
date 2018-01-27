using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class LevelChoseUI : MonoBehaviour {

	[Inject]
	private LevelNumberStartSignal _levelNumberStartSignal;
	
	[Inject]
	private GameStartSignal _gameStartSignal;

	public void StartLevel(int level)
	{
		_levelNumberStartSignal.Fire(level);
	}

	public void StartGame()
	{
		_gameStartSignal.Fire();
	}
}
