using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class LevelWinUI : MonoBehaviour {

	[Inject]
	private LevelStartSignal _levelStartSignal;

	public void StartNextLevel()
	{
		_levelStartSignal.Fire();
	}
}
