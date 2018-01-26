using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    private float _maxSpeed;

	void Start ()
	{
	    _maxSpeed = 0;

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
