using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelLostState : DefaultState {
	override public void Load ()
	{
		SceneManager.LoadScene ("LevelLostScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelLostUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelLostUI");
		SceneManager.UnloadSceneAsync ("LevelLostScene");
	}
}
