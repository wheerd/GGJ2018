﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GateCollider : MonoBehaviour
{
    [Inject]
    private LevelModel _levelModel;

    private PackageColor _color;

    private readonly HashSet<GameObject> _collidedPackages = new HashSet<GameObject>();

    internal void SetColor(PackageColor color)
    {
        _color = color;
    }

    void Update()
    {
        _collidedPackages.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("package") || _collidedPackages.Contains(other.gameObject))
            return;
        
        _collidedPackages.Add(other.gameObject);

        var packageColor = other.gameObject.GetComponent<Package>().Color;

        Destroy(other.gameObject);

        _levelModel.IncrementPackageCount(_color == packageColor);
    }
}
