using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class LevelUI : MonoBehaviour {

	[Inject]
	private LevelWinSignal _levelWinSignal;
	
	[Inject]
	private LevelLostSignal _levelLostSignal;

	public void ShowWin()
	{
		_levelWinSignal.Fire();
	}

	public void ShowLost()
	{
		_levelLostSignal.Fire();
	}
}
