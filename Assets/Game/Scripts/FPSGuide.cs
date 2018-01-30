using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSGuide : MonoBehaviour {

    [SerializeField]
    int _maxFps = 30;

    private void Awake()
    {
        Application.targetFrameRate = _maxFps;
    }
}
