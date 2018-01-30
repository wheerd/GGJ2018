using UnityEngine;
using Zenject;

public class GameConfigInput : MonoBehaviour
{

	[SerializeField] private int MaxLevel = 15;
	[SerializeField] private bool ImmediateFail = false;
	[SerializeField] private float TimeScale = 1;

	[Inject] private GameConfig _gameConfig;
	
	// Use this for initialization
	void Start ()
	{
		_gameConfig.MaxLevel = MaxLevel;
		_gameConfig.ImmediateFail = ImmediateFail;
		_gameConfig.TimeScale = TimeScale;
	}
}
