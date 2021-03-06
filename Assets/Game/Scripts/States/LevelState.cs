using de.deichkrieger.stateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelState : DefaultState
{
	private GameModel _gameModel;
	private GameConfig _gameConfig;
    private LevelModel _levelModel;
    private int _nextLevel;
    private TrackingService _trackingService;
	
	public LevelState(GameModel gameModel, LevelModel levelModel, GameConfig gameConfig, TrackingService trackingService)
	{
		_gameModel = gameModel;
	    _levelModel = levelModel;
		_gameConfig = gameConfig;
        _trackingService = trackingService;
		
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
        SceneManager.LoadScene ("LevelBase", LoadSceneMode.Additive);
        SceneManager.LoadScene ("LevelUI", LoadSceneMode.Additive);

		if (Application.CanStreamedLevelBeLoaded(GetLevelTutorialSceneName()))
		{
			SceneManager.LoadScene(GetLevelTutorialSceneName(), LoadSceneMode.Additive);
		}
		else
		{
			SceneManager.LoadScene("LevelOverlay", LoadSceneMode.Additive);
		}

        _trackingService.LevelStart(_nextLevel, 0);
}

	override public void Unload ()
	{
		if (Application.CanStreamedLevelBeLoaded(GetLevelTutorialSceneName()))
		{
			SceneManager.UnloadSceneAsync(GetLevelTutorialSceneName());
		}
		else
		{
			SceneManager.UnloadSceneAsync ("LevelOverlay");
		}

		SceneManager.UnloadSceneAsync ("LevelUI");
        SceneManager.UnloadSceneAsync ("LevelBase");
        SceneManager.UnloadSceneAsync (GetLevelSceneName());
	}

	private string GetLevelSceneName()
	{
		//return "ManuelTest";
		return string.Format("Level_{0:000}Scene", _gameModel.GetCurrentLevel());
	}
	
	private string GetLevelTutorialSceneName()
	{
		//return "ManuelTest";
		return string.Format("LevelTutorial_{0:000}Scene", _gameModel.GetCurrentLevel());
	}
}
