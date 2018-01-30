using System.Collections;
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

	    text.text = string.Format("Packages delivered: {0}/{1}", _levelModel.CorrectPackageCount, _levelModel.ExpectedPackageCount);
	}
}
