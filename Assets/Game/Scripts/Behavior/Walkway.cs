using UnityEngine;

public class Walkway : MonoBehaviour {

    public float WalkwaySpeed = 5.0f;

    private Material _material;

    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }
    
    private void FixedUpdate()
    {
        UpdateMovingAnimation();
    }

    private void UpdateMovingAnimation()
    {
        var offset = _material.GetTextureOffset("_MainTex");
        offset.y -= Time.fixedDeltaTime * WalkwaySpeed / 2;
        _material.SetTextureOffset("_MainTex", offset);
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
