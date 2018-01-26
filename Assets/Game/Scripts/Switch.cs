using Assets.Game.Scripts;
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

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (packages.Any())
        {
            var package = packages.Dequeue();
            print("Dequeue " + package.transform.name);
            MovePackageToExit(package, SwitchExit);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Package))
        {
            var rigidBody = other.gameObject.GetComponent<Rigidbody>();
            var position = transform.position;
            //position.y++;
            rigidBody.MovePosition(position);
            rigidBody.velocity = Vector3.zero;
            rigidBody.isKinematic = true;
            //rigidBody.MovePosition(transform.position);

            packages.Enqueue(other.gameObject);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    var gameObject = other.gameObject;
    //    if (gameObject.CompareTag(Tags.Package))
    //    {
    //        if (!packages.Contains(gameObject))
    //        {
    //            print("Enqueue " + gameObject.transform.name);
    //            packages.Enqueue(gameObject);
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag(Tags.Package))
    //    {
    //        //packages.Dequeue(other.gameObject);
    //        MovePackageToExit(other.gameObject, SwitchExit);
    //    }
    //}

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

    private void OnCollisionEnter(Collision collision)
    {
        
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
