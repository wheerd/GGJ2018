using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class LevelModel : IInitializable
{
    public int PackageCount { get; private set; }

    public void IncrementPackageCount()
    {
        PackageCount++;
    }

    public void Initialize()
    {
        PackageCount = 0;
    }
}