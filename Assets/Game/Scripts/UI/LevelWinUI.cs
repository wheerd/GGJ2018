using de.deichkrieger.stateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelWinUI : MonoBehaviour
{
	[SerializeField] private AudioClip _levelWinSound;
	[SerializeField] private Text _packets;
	[SerializeField] private Text _tries;
	
	[Inject] private PlayMusicClipSignal _playMusicClipSignal;
    [Inject] private GameHighscoreSignal _gameHighscoreSignal;
    [Inject] private LevelNumberStartSignal _levelStartSignal;
    [Inject] private LevelChoseSignal _levelChoseSignal;

    [Inject] private GameModel _gameModel;
	[Inject] private HighscoreModel _highscoreModel;
	[Inject] private GameConfig _gameConfig;
	[Inject] private LevelModel _levelModel;
	
	void Start()
	{
		var entry = AddHighscore();

        _packets.text = _levelModel.CorrectPackageCount.ToString() + " / " + _levelModel.ExpectedPackageCount;
        _tries.text = entry.HasWon + " / " + entry.Tries;

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

    public void ShowHighscore()
    {
        _gameHighscoreSignal.Fire();
    }

    private string GetTimeFormatted(float time)
	{
		var totalSeconds = (int) time;
		var minutes = totalSeconds / 60;
		var seconds = totalSeconds % 60;

		return string.Format("{0:D}:{1:00}", minutes, seconds);
	}

	private HighscoreEntry AddHighscore()
	{
		return _highscoreModel.AddHighscore(_gameModel.GetLastLevel(), _levelModel.Timer);
	}
}
