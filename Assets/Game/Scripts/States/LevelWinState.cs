using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelWinState : DefaultState {
	
	private GameModel _gameModel;
    private LevelModel _levelModel;
    private TrackingService _trackingService;

    public LevelWinState(GameModel gameModel, LevelModel levelModel, TrackingService trackingService)
    {
        _gameModel = gameModel;
        _levelModel = levelModel;
        _trackingService = trackingService;
    }

    override public void Load ()
	{
		_gameModel.WinCurrentLevel();

        _trackingService.LevelWin(_gameModel.GetLastLevel(), _levelModel.ExpectedPackageCount, _levelModel.CorrectPackageCount, _levelModel.Timer, 0);

        SceneManager.LoadScene ("LevelWinScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelWinUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelWinUI");
		SceneManager.UnloadSceneAsync ("LevelWinScene");
	}
}
