using System.Collections;
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

    private bool _lost = false;

    private MonoBehaviour _monoBehaviour;

    private MonoBehaviour MonoBehaviour
    {
        get { return Object.FindObjectOfType<MonoBehaviour>(); }
    }

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
            Debug.Log("incorrect and immediate fail");
            if (!_lost)
            {
                _lost = true;
                MonoBehaviour.StartCoroutine(LooseLevel());
                Debug.Log("initiate lose level");
            }
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

    private IEnumerator LooseLevel()
    {
        yield return new WaitForSecondsRealtime(2);
        _levelLostSignal.Fire();
        Debug.Log("fire level lost signal");
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
        _lost = false;

        ExpectedPackageCount = Object.FindObjectsOfType<Spawner>().Sum(spawner =>
        {
            if (spawner.SimpleSequenzer)
                return spawner.SimplePaketSequence.Split(',')
                    .Count(e => !string.IsNullOrEmpty(e));
            return spawner.PackageSpawns.Count;
        });
    }
}