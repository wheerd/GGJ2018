using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreEntryRenderer : MonoBehaviour
{

	[SerializeField] private Text _position;
	[SerializeField] private Text _level;
	[SerializeField] private Text _tries;
	
	public void SetData(int position, int level, int wins, int tries)
	{
		_position.text = position.ToString() + ":";
		_level.text = "Level " + level.ToString();
		_tries.text = "Tries " + wins + "/" + tries;
	}
}
