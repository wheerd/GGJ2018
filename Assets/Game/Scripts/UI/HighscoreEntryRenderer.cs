using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreEntryRenderer : MonoBehaviour
{

	[SerializeField] private Text _position;
	[SerializeField] private Text _level;
	[SerializeField] private Text _time;
	
	public void SetData(int position, int level, float time)
	{
		_position.text = position.ToString() + ":";
		_level.text = "Level " + level.ToString();
		_time.text = "Time " + GetTimeFormatted(time);
	}
	
	private string GetTimeFormatted(float time)
	{
		var totalSeconds = (int) time;
		var minutes = totalSeconds / 60;
		var seconds = totalSeconds % 60;

		return string.Format("{0:D}:{1:00}", minutes, seconds);
	}
}
