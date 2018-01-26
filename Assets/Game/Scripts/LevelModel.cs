using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LevelModel : IInitializable
{
    public int PackageCount { get; private set; }

    public float Timer { get; private set; }

    public void IncrementPackageCount()
    {
        PackageCount++;
    }

    public void IncrementTime()
    {
        Timer += Time.deltaTime;
    }

    public void Initialize()
    {
        ResetLevel();
    }

    public void ResetLevel()
    {
        PackageCount = 0;
        Timer = 0;
    }
}