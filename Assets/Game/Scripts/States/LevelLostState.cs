using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelLostState : DefaultState {
	override public void Load ()
	{
		SceneManager.LoadScene ("LevelLostState", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("LevelLostState");
	}
}
