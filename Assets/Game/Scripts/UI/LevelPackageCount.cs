﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelPackageCount : MonoBehaviour
{
    [Inject]
    private LevelModel _levelModel;

	void Update ()
	{
	    var text = GetComponent<Text>();

	    text.text = string.Format("Packages: {0}", _levelModel.PackageCount);
	}
}