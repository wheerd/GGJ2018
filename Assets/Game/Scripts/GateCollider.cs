using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GateCollider : MonoBehaviour
{
    [Inject]
    private LevelModel _levelModel;

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("package"))
            return;

        Destroy(other.gameObject);

        _levelModel.IncrementPackageCount();
    }
}
