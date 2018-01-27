using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class LevelWinUI : MonoBehaviour {

	[Inject]
	GameModel _gameModel;
	
	[Inject]
	GameConfig _gameConfig;
	
	[Inject]
	private LevelNumberStartSignal _levelStartSignal;
	
	[Inject]
	private LevelChoseSignal _levelChoseSignal;

	public void StartNextLevel()
	{
		if (_gameModel.GetLastLevel() >= _gameConfig.MaxLevel)
		{
			_levelChoseSignal.Fire();
		}
		else
		{
			_levelStartSignal.Fire(_gameModel.GetLastLevel() + 1);
		}
	}
}
