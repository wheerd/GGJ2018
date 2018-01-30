﻿using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Package : MonoBehaviour
{
    public PackageColor Color;

    public PackageState State;

    public Material UnknownMaterial;

    private float _maxSpeed;

    private bool _fading;

    private Material _defaultMaterial;

    public void Fade()
    {
        _fading = true;
    }

    void Start ()
    {
        _maxSpeed = 0;
        _defaultMaterial = GetComponent<MeshRenderer>().material;

        UpdateColor();
    }

    public void UpdateMaxBeltSpeed(float speed)
    {
        _maxSpeed = Math.Max(_maxSpeed, speed);
    }

    public void SetState(PackageState state)
    {
        State = state;
        UpdateColor();
    }

    private void UpdateColor()
    {
        var meshRenderer = GetComponent<MeshRenderer>();

        if (State == PackageState.Blank)
        {
            meshRenderer.material = UnknownMaterial;
            return;
        }

        meshRenderer.material = _defaultMaterial;
        meshRenderer.material.color = Color.ToColor();
    }

    void Update()
    {
        var body = GetComponent<Rigidbody>();
        if (!_fading || !body.IsSleeping()) return;
        
        var floorCollider = FindObjectOfType<Floor>().GetComponent<Collider>();
        foreach (var collider in GetComponents<Collider>())
        {
            Physics.IgnoreCollision(floorCollider, collider);
        }

        body.drag = 5;
        Destroy(gameObject, 5);
        _fading = false;
    }

    void LateUpdate ()
    {
        if (_maxSpeed > 0 && !_fading)
        {
            var body = GetComponent<Rigidbody>();

            body.velocity = body.velocity.normalized * _maxSpeed;
            _maxSpeed = 0;
        }
    }
}

public enum PackageState
{
    Normal,
    Blank,
    None
}