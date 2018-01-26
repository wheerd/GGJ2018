using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class HighscoreState : DefaultState {
	override public void Load ()
	{
		SceneManager.LoadScene ("HighscoreScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("HighscoreUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("HighscoreUI");
		SceneManager.UnloadSceneAsync ("HighscoreScene");
	}
}
