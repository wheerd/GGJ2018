using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelWinState : DefaultState {
	override public void Load ()
	{
		SceneManager.LoadScene ("LevelWinScene", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelWinScene");
	}
}
