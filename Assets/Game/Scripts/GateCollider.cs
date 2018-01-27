using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GateCollider : MonoBehaviour
{
    private PackageColor _color;

    private readonly HashSet<GameObject> _packages = new HashSet<GameObject>();

    internal void SetColor(PackageColor color)
    {
        _color = color;
    }

    [Inject]
    private LevelModel _levelModel;

    void Update()
    {
        _packages.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("package") || _packages.Contains(other.gameObject))
            return;
        
        _packages.Add(other.gameObject);

        var packageColor = other.gameObject.GetComponent<Package>().Color;

        Destroy(other.gameObject);

        if (_color == packageColor)
        {
            _levelModel.IncrementPackageCount();
        }
    }
}
