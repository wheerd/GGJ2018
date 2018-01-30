using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

[Serializable]
public class HighscoreEntry
{
	public int Level;
	public float Time;

}

[Serializable]
public class HighscoreModel {

	List<HighscoreEntry> Highscores = new List<HighscoreEntry>();

	public HighscoreModel()
	{
		RestoreHighscore();
	}

	public List<HighscoreEntry> GetScores()
	{
		return Highscores;
	}

	public void AddHighscore(int level, float time)
	{
		Debug.Log("Add Highscore " + level + "/" + time);
		Highscores.Add(new HighscoreEntry()
		{
			Level = level,
			Time = time
		});

		Highscores = Highscores.OrderByDescending(o => o.Level).
			ThenBy(o => o.Time).ToList();
		
		SaveHighscore();
	}
	
	class ListHolder
	{
		public List<HighscoreEntry> list;
	}
	
	private void SaveHighscore()
	{
		ListHolder listHolder = new ListHolder();
		listHolder.list = Highscores;
		
		string json = JsonUtility.ToJson(listHolder);
		PlayerPrefs.SetString("highscore", json);
		
		Debug.Log("save highscore " + json);
	}

	private void RestoreHighscore()
	{		
		string json = PlayerPrefs.GetString("highscore");
		
		Debug.Log("restore highscore " + json);
		
		if (json.IsEmpty())
		{
			return;
		}

		ListHolder listModel = JsonUtility.FromJson<ListHolder>(json);
		Highscores = listModel.list;
	}

	public void ResetState()
	{
		Highscores = new List<HighscoreEntry>();
		SaveHighscore();
	}
}
