using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelState : DefaultState {
	override public void Load ()
	{
		SceneManager.LoadScene ("Level_001Scene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelUI");
		SceneManager.UnloadSceneAsync ("Level_001Scene");
	}
}
