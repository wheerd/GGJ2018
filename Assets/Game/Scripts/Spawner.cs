using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float _elapsed = 0;

    public float Speed = 5;

    public float SpawnRate = 0.5f;

    public GameObject Package;
    
    void Start ()
	{
	    _elapsed = 0;
	}
	
	void Update ()
	{
	    _elapsed += Time.deltaTime;

	    while (_elapsed > SpawnRate)
	    {
	        _elapsed -= SpawnRate;
            
	        var position = transform.position + transform.up * 2;

	        Instantiate(Package, position, Quaternion.identity);
	    }
	}
    void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("package"))
            return;
        
        collision.rigidbody.velocity = Speed * transform.forward;
    }
}
