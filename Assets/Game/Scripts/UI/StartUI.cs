using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;

public class StartUI : MonoBehaviour {

	[Inject]
	GameModel _gameModel;
	
	[Inject]
	private GameHighscoreSignal _highscoreSignal;
	
	[Inject]
	private GameCreditsSignal _creditsSignal;
	
	[Inject]
	private LevelStartSignal _levelStartSignal;
	
	[Inject]
	private LevelChoseSignal _levelChoseSignal;

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
	
	public void ChoseLevel()
	{
		_levelChoseSignal.Fire();
	}

	public void ResetState()
	{
		_gameModel.ResetState();
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
