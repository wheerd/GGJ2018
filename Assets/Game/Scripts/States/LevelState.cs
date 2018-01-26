using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelState : DefaultState
{
	private GameModel _gameModel;
	
	public LevelState(GameModel gameModel)
	{
		_gameModel = gameModel;
	}
	
	override public void Load ()
	{
		_gameModel.PlayNextLevel(_gameModel.GetLastFinishedLevel() + 1);
		
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
