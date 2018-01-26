using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class StartState : DefaultState
{

	override public void Load ()
	{
		SceneManager.LoadScene ("StartScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("StartUI", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("StartUI");
		SceneManager.UnloadSceneAsync ("StartScene");
	}
}
