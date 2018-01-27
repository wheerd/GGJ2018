using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBottom : MonoBehaviour
{
    public float OutputAngle;

    public float Speed = 5.0f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("package"))
        {
            var entrance = transform.position - transform.forward;
            var outVector = Quaternion.Euler(0, OutputAngle, 0) * transform.forward;
            var exit = transform.position + outVector;

            var distanceToExit = (exit - collision.rigidbody.position).sqrMagnitude;
            var distanceToEntrance = (entrance - collision.rigidbody.position).sqrMagnitude;

            var total = distanceToExit + distanceToEntrance;
            /*
            var direction = transform.forward * distanceToEntrance / total
                           + outVector * distanceToEntrance / total;
            */

            var direction = distanceToExit > distanceToEntrance ? transform.forward : outVector;

            collision.gameObject.GetComponent<Package>().UpdateMaxBeltSpeed(Speed);
            collision.rigidbody.velocity += direction * Speed;
        }
    }
}
