using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class StartState : DefaultState
{

	override public void Load ()
	{
		SceneManager.LoadScene ("StartScene", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("StartScene");
	}
}
