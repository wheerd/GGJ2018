using Assets.Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Merger : MonoBehaviour
{
    public float TimeBefore;

    private int _index;

    private float _elapsed;

    public float OutputSpeed = 5.0f;

    private readonly Queue<GameObject> packageQueue = new Queue<GameObject>();

    private readonly HashSet<GameObject> ignored = new HashSet<GameObject>();

    private void Start ()
    {
        
    }

    private void Update ()
    {
        if (!packageQueue.Any())
        {
            return;
        }

        _elapsed += Time.deltaTime;

        var nextSpawnTime = TimeBefore;

        if (!(_elapsed > nextSpawnTime)) return;

        _elapsed -= nextSpawnTime;

        var package = packageQueue.Dequeue();
        MovePackageToExit(package);
    }

    private void MovePackageToExit(GameObject gameObject)
    {
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.detectCollisions = true;
        rigidBody.velocity = transform.forward * OutputSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Package))
        {
            if (ignored.Contains(other.gameObject)) return;
            ignored.Add(other.gameObject);

            var position = transform.position;
            var rigidBody = other.gameObject.GetComponent<Rigidbody>();

            rigidBody.velocity = Vector3.zero;
            rigidBody.MovePosition(position);
            rigidBody.isKinematic = true;
            
            var collider = other.gameObject.GetComponent<Collider>();
            foreach (var item in packageQueue)
            {
                var otherCollider = item.gameObject.gameObject.GetComponent<Collider>();
                Physics.IgnoreCollision(collider, otherCollider);
            }

            packageQueue.Enqueue(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Package))
        {
            ignored.Remove(other.gameObject);
        }
    }
}
