using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class LevelUI : MonoBehaviour {

	[Inject]
	private LevelWinSignal _levelWinSignal;
	
	[Inject]
	private LevelLostSignal _levelLostSignal;
	
	[Inject] private PauseSignal _pauseSignal;

	public void Pause()
	{
		_pauseSignal.Fire();
	}

	public void ShowWin()
	{
		_levelWinSignal.Fire();
	}

	public void ShowLost()
	{
		_levelLostSignal.Fire();
	}
}
