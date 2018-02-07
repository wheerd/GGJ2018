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
    public int HasWon = 0;
    public int Tries = 0;

}

[Serializable]
public class HighscoreModel {

	List<HighscoreEntry> Highscores = new List<HighscoreEntry>();
    int Version = 1;

	public HighscoreModel()
	{
		RestoreHighscore();
	}

	public List<HighscoreEntry> GetScores()
	{
		return Highscores;
	}

    public HighscoreEntry AddTry(int level)
    {
        Debug.Log("Add Try " + level);

        var entry = Highscores.Find(q => q.Level == level);

        if (entry != null)
        {
            entry.Tries++;
        }
        else
        {
            entry = new HighscoreEntry()
            {
                Level = level,
                Time = 0,
                Tries = 1,
                HasWon = 0
            };

            Highscores.Add(entry);
        }

        SaveHighscore();

        return entry;
    }

    public HighscoreEntry AddHighscore(int level, float time)
	{
		Debug.Log("Add Highscore " + level + "/" + time);

        var entry = Highscores.Find(q => q.Level == level);

        if (entry != null)
        {
            entry.Tries++;
            entry.HasWon++;

            if (entry.Time > time)
            {
                entry.Time = time;
            }
        }
        else
        {
            entry = new HighscoreEntry()
            {
                Level = level,
                Time = time,
                Tries = 1,
                HasWon = 1
            };

            Highscores.Add(entry);
        }

		SaveHighscore();

        return entry;
	}
	
	class ListHolder
	{
		public List<HighscoreEntry> list;
        public int Version = 0;
	}
	
	private void SaveHighscore()
	{
        Highscores = Highscores.DistinctBy(o => o.Level).OrderByDescending(o => o.Level).ThenBy(o => o.Time).ToList();

        ListHolder listHolder = new ListHolder();
		listHolder.list = Highscores;
        listHolder.Version = Version;
		
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
		Highscores = Migrate(listModel.list, listModel.Version);

        Highscores = Highscores.DistinctBy(o => o.Level).OrderByDescending(o => o.Level).ThenBy(o => o.Time).ToList();
    }

    public void ResetState()
	{
		Highscores = new List<HighscoreEntry>();
		SaveHighscore();
	}

    private List<HighscoreEntry> Migrate(List<HighscoreEntry> list, int version)
    {
        if (version == 0)
        {
            Debug.Log("Migrate Highscore from version 0");
            foreach(var entry in list)
            {
                if (entry.Tries == 0)
                {
                    entry.Tries = 1;
                }

                if (entry.HasWon == 0)
                {
                    entry.HasWon = 1;
                }
            }
        }

        return list;
    }
}
