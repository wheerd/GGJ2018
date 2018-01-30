using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltCorner : MonoBehaviour {

    public float Speed = 5.0f;

    void Update()
    {
        var material = GetComponent<MeshRenderer>().material;
        var offset = material.GetTextureOffset("_MainTex");
        offset.y -= Time.deltaTime * Speed / 2;
        material.SetTextureOffset("_MainTex", offset);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("package"))
        {
            var entrance = transform.position + transform.right;
            var exit = transform.position + transform.forward;

            var distanceToExit = (exit - collision.rigidbody.position).sqrMagnitude;
            var distanceToEntrance = (entrance - collision.rigidbody.position).sqrMagnitude;

            var direction = distanceToExit < distanceToEntrance ? transform.forward : -transform.right;

            collision.gameObject.GetComponent<Package>().UpdateMaxBeltSpeed(Speed);
            collision.rigidbody.velocity += direction * Speed;
        }
    }
}
