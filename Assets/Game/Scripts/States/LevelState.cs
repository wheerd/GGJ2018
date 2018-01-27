using de.deichkrieger.stateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelState : DefaultState
{
	private GameModel _gameModel;
	private int _nextLevel;
	
	public LevelState(GameModel gameModel)
	{
		_gameModel = gameModel;
		SetNextLevel(_gameModel.GetLastFinishedLevel() + 1);
	}

	public void SetNextLevel(int nextLevel)
	{
		_nextLevel = nextLevel;
	}
	
	override public void Load ()
	{
		_gameModel.PlayNextLevel(_nextLevel);
		
		SceneManager.LoadScene (GetLevelSceneName(), LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelUI");
		SceneManager.UnloadSceneAsync (GetLevelSceneName());
	}

	private string GetLevelSceneName()
	{
		return string.Format("Level_{0:000}Scene", _gameModel.GetCurrentLevel());
	}
}
