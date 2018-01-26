using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelWinState : DefaultState {
	
	private GameModel _gameModel;
	
	public LevelWinState(GameModel gameModel)
	{
		_gameModel = gameModel;
	}
	
	override public void Load ()
	{
		_gameModel.WinCurrentLevel();
		
		SceneManager.LoadScene ("LevelWinScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelWinUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelWinUI");
		SceneManager.UnloadSceneAsync ("LevelWinScene");
	}
}
