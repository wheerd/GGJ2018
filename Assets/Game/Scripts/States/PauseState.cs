using UnityEngine;

namespace de.deichkrieger.stateMachine
{
	public class PauseState : DefaultState
	{

		override public void Load ()
		{
			Debug.Log ("Pause game");
			Time.timeScale = 0;
		}

		override public void Unload ()
		{
			Debug.Log ("Resume game");
			Time.timeScale = 1;
		}

	}
}