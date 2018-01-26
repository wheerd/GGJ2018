using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelChoseState : DefaultState {
	override public void Load ()
	{
		SceneManager.LoadScene ("LevelChoseScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelChoseUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelChoseUI");
		SceneManager.UnloadSceneAsync ("LevelChoseScene");
	}
}
