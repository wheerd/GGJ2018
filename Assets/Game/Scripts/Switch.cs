﻿using Assets.Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public SwitchType SwitchType;

    public SwitchExit SwitchExit;

    public float OutputSpeed = 5.0f;

    private Queue<GameObject> packages = new Queue<GameObject>();

    private void Start()
    {
        UpdateSwitchExit();
        UpdateRotation();
    }

    private void Update ()
    {
        if (packages.Any())
        {
            var package = packages.Dequeue();
            print("Dequeue " + package.transform.name);
            MovePackageToExit(package, SwitchExit);
        }
    }

    private void OnMouseDown()
    {
        UpdateSwitchExit();
    }

    private void UpdateSwitchExit()
    {
        SwitchExit newExit;
        var values = Enum.GetValues(typeof(SwitchExit)).Cast<SwitchExit>();
        var currentExit = SwitchExit;

        if (currentExit == values.Last())
        {
            newExit = values.First();
        }
        else
        {
            newExit = currentExit + 1;
        }

        if (SwitchType != SwitchType.ThreeWay && newExit == SwitchExit.Ahead)
        {
            newExit++;
        }

        SetSwitchExit(newExit);
    }

    private void SetSwitchExit(SwitchExit switchExit)
    {
        SwitchExit = switchExit;
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        var top = transform.GetChild(1);
        float yAngle;

        switch (SwitchExit)
        {
            case SwitchExit.Ahead:
                yAngle = 180;
                break;
            case SwitchExit.Left:
                yAngle = 90;
                break;
            case SwitchExit.Right:
                yAngle = -90;
                break;
            default:
                throw new Exception();
        }
        
        top.transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Package))
        {
            var position = transform.position;
            var rigidBody = other.gameObject.GetComponent<Rigidbody>();

            rigidBody.velocity = Vector3.zero;
            rigidBody.MovePosition(position);
            rigidBody.isKinematic = true;

            packages.Enqueue(other.gameObject);
        }
    }

    private void MovePackageToExit(GameObject gameObject, SwitchExit exit)
    {
        print(string.Format("Moving package {0} to exit {1}", gameObject.transform.name, exit));

        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;

        switch (exit)
        {
            case SwitchExit.Ahead:
                rigidBody.velocity = transform.forward * OutputSpeed;
                break;
            case SwitchExit.Left:
                rigidBody.velocity = transform.right * -1 * OutputSpeed;
                break;
            case SwitchExit.Right:
                rigidBody.velocity = transform.right * OutputSpeed;
                break;
            default:
                throw new ExitGUIException();
        }
    }
}

public enum SwitchType
{
    TwoWay,
    ThreeWay
}

public enum SwitchExit
{
    Ahead,
    Left,
    Right
}