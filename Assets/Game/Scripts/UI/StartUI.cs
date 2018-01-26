using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class StartUI : MonoBehaviour {

	[Inject]
	private GameHighscoreSignal _highscoreSignal;
	
	[Inject]
	private GameCreditsSignal _creditsSignal;
	
	[Inject]
	private LevelStartSignal _levelStartSignal;

	public void ShowHighscore()
	{
		_highscoreSignal.Fire();
	}

	public void ShowCredits()
	{
		_creditsSignal.Fire();
	}

	public void StartNextLevel()
	{
		_levelStartSignal.Fire();
	}
}
