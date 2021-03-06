using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Floor : MonoBehaviour {

    [Inject]
    private LevelModel _levelModel;

    private readonly HashSet<GameObject> _collidedPackages = new HashSet<GameObject>();
    
    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("package") || _collidedPackages.Contains(other.gameObject))
            return;

        _collidedPackages.Add(other.gameObject);
        _levelModel.IncrementPackageCount(false);
    }
}
