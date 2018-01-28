using de.deichkrieger.stateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelLostUI : MonoBehaviour {
	[SerializeField] private Text _packets;
	[SerializeField] private Text _time;
	[SerializeField] private AudioClip _levelLostSound;
	
	[Inject] private GameStartSignal _gameStartSignal;
	
	[Inject] private GameHighscoreSignal _gameHighscoreSignal;
	
	[Inject] private LevelNumberStartSignal _levelNumberStartSignal;
	
	[Inject] private LevelModel _levelModel;
	[Inject] private GameModel _gameModel;

	[Inject] private PlayMusicClipSignal _playMusicClipSignal;

	void Start()
	{
		_packets.text = _levelModel.CorrectPackageCount.ToString() + " / " + _levelModel.ExpectedPackageCount;
		_time.text = GetTimeFormatted(_levelModel.Timer);
		
		_playMusicClipSignal.Fire(_levelLostSound);
	}

	public void Replay()
	{
		_levelNumberStartSignal.Fire(_gameModel.GetLastLevel());
	}
	
	public void StartGame()
	{
		_gameStartSignal.Fire();
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
}
