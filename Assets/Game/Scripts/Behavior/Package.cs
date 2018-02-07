using System;
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

    private Rigidbody _rigidbody;

    private MeshRenderer _meshRenderer;

    void Start ()
    {
        _maxSpeed = 0;
        _defaultMaterial = GetComponent<MeshRenderer>().material;
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();

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
        if (State == PackageState.Blank)
        {
            _meshRenderer.material = UnknownMaterial;
            return;
        }

        _meshRenderer.material = _defaultMaterial;
        _meshRenderer.material.color = Color.ToColor();
    }

    void FixedUpdate()
    {
        if (!_fading || !_rigidbody.IsSleeping()) return;
        
        var floorCollider = FindObjectOfType<Floor>().GetComponent<Collider>();
        foreach (var myCollider in GetComponents<Collider>())
        {
            Physics.IgnoreCollision(floorCollider, myCollider);
        }

        _rigidbody.drag = 5;
        Destroy(gameObject, 5);
        _fading = false;
    }
    
    void LateUpdate()
    {
        if (_maxSpeed > 0 && !_fading)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
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
