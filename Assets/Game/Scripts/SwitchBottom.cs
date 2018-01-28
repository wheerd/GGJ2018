using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBottom : MonoBehaviour
{
    public float Speed = 5.0f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("package"))
        {
            var inDirection = transform.parent.transform.forward;
            var entrance = transform.position - inDirection;
            var outDirection = transform.forward;
            var exit = transform.position + outDirection;

            var distanceToExit = (exit - collision.rigidbody.worldCenterOfMass).sqrMagnitude;
            var distanceToEntrance = (entrance - collision.rigidbody.worldCenterOfMass).sqrMagnitude;

            var direction = distanceToExit > distanceToEntrance ? inDirection : outDirection;

            collision.gameObject.GetComponent<Package>().UpdateMaxBeltSpeed(Speed);
            collision.rigidbody.velocity += direction * Speed;
        }
    }
}
