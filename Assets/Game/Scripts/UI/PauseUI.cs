using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class PauseUI : MonoBehaviour {

	[Inject] private PauseSignal _pauseSignal;

	public void Unpause()
	{
		_pauseSignal.Fire();
	}
}
