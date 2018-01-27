using System;
using System.Collections.Generic;
using System.Linq;
using de.deichkrieger.stateMachine;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class LevelModel : IInitializable
{
    [Inject] private GameConfig _gameConfig;
    
    public int CorrectPackageCount { get; private set; }

    public int TotalPackageCount { get; private set; }

    public int ExpectedPackageCount { get; private set; }

    public float Timer { get; private set; }

    private readonly LevelWinSignal _levelWinSignal;
    private readonly LevelLostSignal _levelLostSignal;

    public LevelModel(LevelWinSignal levelWinSignal, LevelLostSignal levelLostSignal)
    {
        _levelWinSignal = levelWinSignal;
        _levelLostSignal = levelLostSignal;
    }

    public void IncrementPackageCount(bool correct)
    {
        if(correct) CorrectPackageCount++;
        TotalPackageCount++;

        CheckGameEnd(correct);
    }

    private void CheckGameEnd(bool lastWasCorrect)
    {
        if (_gameConfig.ImmediateFail && !lastWasCorrect)
        {
            _levelLostSignal.Fire();
        }
        
        
        if (TotalPackageCount != ExpectedPackageCount) return;

        if (CorrectPackageCount == ExpectedPackageCount)
        {
            _levelWinSignal.Fire();
        }
        else
        {
            _levelLostSignal.Fire();
        }
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
        CorrectPackageCount = 0;
        TotalPackageCount = 0;
        Timer = 0;
        ExpectedPackageCount = Object.FindObjectsOfType<Spawner>().Sum(s => s.PackageSpawns.Count);
    }
}