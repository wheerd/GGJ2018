using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelLostState : DefaultState {
	
	private GameModel _gameModel;
	
	public LevelLostState(GameModel gameModel)
	{
		_gameModel = gameModel;
	}
	
	override public void Load ()
	{
		_gameModel.LoseCurrentLevel();
		
		SceneManager.LoadScene ("LevelLostScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelLostUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelLostUI");
		SceneManager.UnloadSceneAsync ("LevelLostScene");
	}
}
