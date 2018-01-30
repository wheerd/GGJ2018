using UnityEngine;

public class Walkway : MonoBehaviour {

    public float WalkwaySpeed = 5.0f;

    private void FixedUpdate()
    {
        UpdateMovingAnimation();
    }

    private void UpdateMovingAnimation()
    {
        var material = GetComponent<MeshRenderer>().material;
        var offset = material.GetTextureOffset("_MainTex");
        offset.y -= Time.fixedDeltaTime * WalkwaySpeed / 2;
        material.SetTextureOffset("_MainTex", offset);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("package"))
        {
            collision.gameObject.GetComponent<Package>().UpdateMaxBeltSpeed(WalkwaySpeed);
            collision.rigidbody.velocity += transform.forward * WalkwaySpeed;
        }
    }
}
