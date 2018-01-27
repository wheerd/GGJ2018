using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Serializable]
    public struct PackageSpawn
    {
        public float TimeBefore;

        public PackageColor Color;
    }

    private int _index;

    private float _elapsed;

    public float Speed = 5;

    public GameObject Package;

    public List<PackageSpawn> PackageSpawns = new List<PackageSpawn>();
    
    void Start ()
    {
        _index = 0;
        _elapsed = 0;
    }

    void AnimateTexture()
    {
        var material = GetComponent<MeshRenderer>().material;
        var offset = material.GetTextureOffset("_MainTex");
        offset.y -= Time.deltaTime * Speed / 2;
        material.SetTextureOffset("_MainTex", offset);
    }

    void Update ()
    {
        AnimateTexture();

        _elapsed += Time.deltaTime;

	    if (_index >= PackageSpawns.Count)
	    {
	        return;
	    }

        var nextSpawnTime = PackageSpawns[_index].TimeBefore;
	    var nextColor = PackageSpawns[_index].Color;

	    if (!(_elapsed > nextSpawnTime)) return;

	    _elapsed -= nextSpawnTime;
            
	    var position = transform.position + transform.up * 2;

	    var package = Instantiate(Package, position, Quaternion.identity);

	    package.GetComponent<Package>().Color = nextColor;
	    package.name = string.Format("Package {0}", _index);

        _index++;
	}

    void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("package"))
            return;
        
        collision.rigidbody.velocity = Speed * transform.forward;
    }
}
