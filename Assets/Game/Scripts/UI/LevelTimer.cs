using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelTimer : MonoBehaviour
{
    [Inject]
    private LevelModel _levelModel;
    
	void Start () {
		_levelModel.ResetLevel();
	}
	
	void Update ()
	{
        _levelModel.IncrementTime();

	    var totalSeconds = (int) _levelModel.Timer;
	    var minutes = totalSeconds / 60;
	    var seconds = totalSeconds % 60;

	    GetComponent<Text>().text = string.Format("{0:D}:{1:00}", minutes, seconds);
	}
}
