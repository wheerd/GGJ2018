using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using de.deichkrieger.stateMachine;

public class FirstPersonState : DefaultState
{

	override public void Load ()
	{
		SceneManager.LoadScene ("FirstPersonScene", LoadSceneMode.Additive);
	}

	override public void Unload ()
	{
		SceneManager.UnloadSceneAsync ("FirstPersonScene");
	}
}
