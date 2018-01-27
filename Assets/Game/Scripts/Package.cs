using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Package : MonoBehaviour
{
    private float _maxSpeed;

    public PackageColor Color;

    private bool _fading;

    public void Fade()
    {
        _fading = true;
    }

    void Start ()
    {
        _maxSpeed = 0;
        transform.localScale.Scale(new Vector3(
            Random.Range(0.9f, 1.1f),
            Random.Range(0.9f, 1.1f),
            Random.Range(0.9f, 1.1f)));

        GetComponent<MeshRenderer>().material.color = Color.ToColor();
    }

    public void UpdateMaxBeltSpeed(float speed)
    {
        _maxSpeed = Math.Max(_maxSpeed, speed);
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
