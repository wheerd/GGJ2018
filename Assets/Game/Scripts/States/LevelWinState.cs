using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelWinState : DefaultState {
	override public void Load ()
	{
		SceneManager.LoadScene ("LevelWinScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelWinUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelWinUI");
		SceneManager.UnloadSceneAsync ("LevelWinScene");
	}
}
