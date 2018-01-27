using de.deichkrieger.stateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelState : DefaultState
{
	private GameModel _gameModel;
	private GameConfig _gameConfig;
    private LevelModel _levelModel;
    private int _nextLevel;
	
	public LevelState(GameModel gameModel, LevelModel levelModel, GameConfig gameConfig)
	{
		_gameModel = gameModel;
	    _levelModel = levelModel;
		_gameConfig = gameConfig;
		
	    SetNextLevel(_gameModel.GetLastFinishedLevel() + 1);
	}

	public void SetNextLevel(int nextLevel)
	{
		_nextLevel = nextLevel;
	}
	
	override public void Load ()
	{
		_gameModel.PlayNextLevel(_nextLevel);
	    _levelModel.ResetLevel();

		Time.timeScale = _gameConfig.TimeScale;
		Time.maximumDeltaTime = _gameConfig.TimeScale;
		Time.fixedDeltaTime = _gameConfig.TimeScale * 0.02f;


        SceneManager.LoadScene (GetLevelSceneName(), LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelUI", LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelOverlay", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelOverlay");
		SceneManager.UnloadSceneAsync ("LevelUI");
		SceneManager.UnloadSceneAsync (GetLevelSceneName());
	}

	private string GetLevelSceneName()
	{
		//return "ManuelTest";
		return string.Format("Level_{0:000}Scene", _gameModel.GetCurrentLevel());
	}
}
