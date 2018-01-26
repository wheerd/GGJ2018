using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using de.deichkrieger.stateMachine;

public class ClickToMoveState : DefaultState
{

	override public void Load ()
	{
		SceneManager.LoadScene ("ClickToMoveScene", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("ClickToMoveScene");
	}
}
