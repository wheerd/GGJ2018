using de.deichkrieger.stateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelWinUI : MonoBehaviour
{
	[SerializeField] private AudioClip _levelWinSound;
	[SerializeField] private Text _packets;
	[SerializeField] private Text _time;
	
	[Inject] private PlayMusicClipSignal _playMusicClipSignal;
	
	[Inject]
	GameModel _gameModel;
	
	[Inject]
	HighscoreModel _highscoreModel;
	
	[Inject]
	GameConfig _gameConfig;

	[Inject] private LevelModel _levelModel;
	
	[Inject]
	private LevelNumberStartSignal _levelStartSignal;
	
	[Inject]
	private LevelChoseSignal _levelChoseSignal;

	void Start()
	{
		_packets.text = _levelModel.CorrectPackageCount.ToString() + " / " + _levelModel.ExpectedPackageCount;
		_time.text = GetTimeFormatted(_levelModel.Timer);
		
		AddHighscore();
		
		_playMusicClipSignal.Fire(_levelWinSound);
	}
	
	public void StartNextLevel()
	{
		if (_gameModel.GetLastLevel() >= _gameConfig.MaxLevel)
		{
			_levelChoseSignal.Fire();
		}
		else
		{
			_levelStartSignal.Fire(_gameModel.GetLastLevel() + 1);
		}
	}

	private string GetTimeFormatted(float time)
	{
		var totalSeconds = (int) time;
		var minutes = totalSeconds / 60;
		var seconds = totalSeconds % 60;

		return string.Format("{0:D}:{1:00}", minutes, seconds);
	}

	private void AddHighscore()
	{
		_highscoreModel.AddHighscore(_gameModel.GetLastLevel(), _levelModel.Timer);
	}
}
