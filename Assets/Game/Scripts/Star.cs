using UnityEngine;

public class Star : MonoBehaviour
{
    public float Speed = 50;

	void Update ()
	{
		transform.RotateAround(transform.position, Vector3.up, Speed * Time.deltaTime);
	    transform.RotateAround(transform.position, Vector3.forward, Speed / 3 * Time.deltaTime);
	    transform.RotateAround(transform.position, Vector3.right, Speed / 2 * Time.deltaTime);
    }
}
