using UnityEngine;

public class Walkway : MonoBehaviour {

    public float WalkwaySpeed = 5.0f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("package"))
        {
            collision.gameObject.GetComponent<Package>().UpdateMaxBeltSpeed(WalkwaySpeed);
            collision.rigidbody.velocity += transform.forward * WalkwaySpeed;
        }
    }
}
