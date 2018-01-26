using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject;

public class GameModel
{
	private List<int> _finishedLevels = new List<int>(); 
	private int _currentLevel = 0;
	private int _lastLevel = 0;

	public GameModel()
	{
		RestorePlayedLevels();
	}
	
	public int GetCurrentLevel()
	{
		return _currentLevel;
	}

	public List<int> GetFinishedLevels()
	{
		return _finishedLevels.OrderBy(o => o).ToList();
	}

	public int GetMaxPlayableLevel()
	{
		int count = _finishedLevels.Count;

		if (count == 0)
		{
			return 1;
		} 
		
		if (count == 1)
		{
			return 2;
		}
		
		return _finishedLevels.Count + 3;
	}

	public int GetLastFinishedLevel()
	{
		if (GetFinishedLevels().Count == 0)
		{
			return 0;
		}

		return GetFinishedLevels().Last();
	}
	
	public void PlayNextLevel(int level)
	{
		if (IsLevelAllowed(level))
		{
			_currentLevel = level;
			_lastLevel = _currentLevel;
		}
		else
		{
			throw new ArgumentException("level is not allowed to play");
		}
	}

	private bool IsLevelAllowed(int level)
	{
		if (level == 1)
		{
			return true;
		} 
		
		if (level > 1 || level <= 2) // tutorials
		{
			return _finishedLevels.Contains(level - 1);
		}
		
		if (level >= 3 && level <= 5)
		{
			return _finishedLevels.Contains(2);
		} 
		
		return (level - 3) >= _finishedLevels.Count;
	}
	
	public void LoseCurrentLevel()
	{
		_currentLevel = 0;
	}
	
	public void WinCurrentLevel()
	{
		if (_currentLevel != 0)
		{
			_finishedLevels.Add(_currentLevel);
			_currentLevel = 0;

			SavePlayedLevels();
		}
		else
		{
			throw new ArgumentException("player is not playing a level");
		}
	}

	class ListHolder
	{
		public List<int> list;
	}

	private void SavePlayedLevels()
	{
		ListHolder listHolder = new ListHolder();
		listHolder.list = _finishedLevels;
			
		string json = JsonUtility.ToJson(listHolder);
		PlayerPrefs.SetString("finished_levels", json);
		
		Debug.Log("save levels " + json);
	}

	private void RestorePlayedLevels()
	{		
		string json = PlayerPrefs.GetString("finished_levels");
		
		Debug.Log("restore levels " + json);
		
		if (json.IsEmpty())
		{
			return;
		}

		ListHolder listHolder = JsonUtility.FromJson<ListHolder>(json);
		_finishedLevels = listHolder.list;
	}
	
	
}
