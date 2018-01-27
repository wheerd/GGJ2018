using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScrollPanel : MonoBehaviour
{

	[Inject]
	GameModel _gameModel;
	
	[Inject]
	GameConfig _gameConfig;

	[SerializeField]
	LevelButton _levelButtonPrefab;
	
	void Start()
	{
		List<int> finishedLevels = _gameModel.GetFinishedLevels();
		int maxLevel = _gameConfig.MaxLevel;
		
		for (int i = 1; i <= maxLevel; i++)
		{
			LevelButton newButton = Instantiate(_levelButtonPrefab);
			newButton.SetLevel(i);

			if (finishedLevels.Contains(i))
			{
				newButton.SetPlayed();
			} else if (i <= _gameModel.GetMaxPlayableLevel())
			{
				newButton.SetPlayable();
			}
			else
			{
				newButton.SetBlocked();
			}
			
			newButton.gameObject.SetActive(true);
			newButton.gameObject.transform.SetParent(transform);
		}
	}
}
