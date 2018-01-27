using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class LevelChoseUI : MonoBehaviour {

	[Inject]
	private LevelNumberStartSignal _levelNumberStartSignal;

	public void StartLevel(int level)
	{
		_levelNumberStartSignal.Fire(level);
	}
}
