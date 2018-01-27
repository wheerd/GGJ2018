using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Random = UnityEngine.Random;

public class Package : MonoBehaviour
{
    private float _maxSpeed;

    public Color Color;

	void Start ()
	{
	    _maxSpeed = 0;
        transform.localScale.Scale(new Vector3(
            Random.Range(0.9f, 1.1f),
            Random.Range(0.9f, 1.1f),
            Random.Range(0.9f, 1.1f)));

	    GetComponent<MeshRenderer>().material.color = Color;
	}

    public void UpdateMaxBeltSpeed(float speed)
    {
        _maxSpeed = Math.Max(_maxSpeed, speed);
    }
	
	void LateUpdate ()
	{
	    if (_maxSpeed > 0)
	    {
	        var body = GetComponent<Rigidbody>();

	        body.velocity = body.velocity.normalized * _maxSpeed;
	        _maxSpeed = 0;
	    }
	}
}
