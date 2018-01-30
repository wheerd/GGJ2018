using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelLostState : DefaultState {
	
	private GameModel _gameModel;
    private LevelModel _levelModel;
    private TrackingService _trackingService;
	
	public LevelLostState(GameModel gameModel, LevelModel levelModel, TrackingService trackingService)
	{
		_gameModel = gameModel;
        _levelModel = levelModel;
        _trackingService = trackingService;
	}
	
	override public void Load ()
	{
		_gameModel.LoseCurrentLevel();

        _trackingService.LevelLose(_gameModel.GetLastLevel(), _levelModel.ExpectedPackageCount, _levelModel.CorrectPackageCount, _levelModel.Timer);
		
		SceneManager.LoadScene ("LevelLostScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelLostUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelLostUI");
		SceneManager.UnloadSceneAsync ("LevelLostScene");
	}
}
