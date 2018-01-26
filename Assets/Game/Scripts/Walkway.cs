using UnityEngine;

public class Walkway : MonoBehaviour {

    public float WalkwaySpeed = 5.0f;

    private void OnCollisionEnter(Collision collision)
    {
        print("Collision occured with " + collision.transform.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("package"))
        {
            collision.rigidbody.velocity = transform.forward * WalkwaySpeed;
            //collision.rigidbody.AddForce(transform.forward * WalkwaySpeed, ForceMode.Acceleration);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        print("No longer in contact with " + collision.transform.name);
    }
}
