using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HighscoreListRenderer : MonoBehaviour
{

	[SerializeField] private HighscoreEntryRenderer _entryPrefab;

	[Inject] private HighscoreModel _highscoreModel;

	void Start()
	{
		var i = 1;
		foreach (var entry in _highscoreModel.GetScores())
		{
			HighscoreEntryRenderer item = Instantiate(_entryPrefab);
			
			item.SetData(i++, entry.Level, entry.HasWon, entry.Tries);
			
			item.gameObject.SetActive(true);
			item.gameObject.transform.SetParent(transform);
			
			item.transform.localScale = new Vector3( 1, 1, 1 );
			item.transform.localPosition = Vector3.zero;
		}
	}
}
