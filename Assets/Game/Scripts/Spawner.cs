using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Serializable]
    public struct PackageSpawn
    {
        public float TimeBefore;

        public PackageColor Color;

        public PackageState State;
    }

    private int _index;

    private float _elapsed;

    public float Speed = 5;

    public GameObject Package;

    public List<PackageSpawn> PackageSpawns = new List<PackageSpawn>();

    private void Start ()
    {
        _index = 0;
        _elapsed = 0;
    }

    private void AnimateTexture()
    {
        var material = GetComponent<MeshRenderer>().material;
        var offset = material.GetTextureOffset("_MainTex");
        offset.y -= Time.deltaTime * Speed / 2;
        material.SetTextureOffset("_MainTex", offset);
    }

    private void Update ()
    {
        //AnimateTexture();

        _elapsed += Time.deltaTime;

        if (_index >= PackageSpawns.Count)
        {
            return;
        }

        var nextSpawnTime = PackageSpawns[_index].TimeBefore;
        var nextColor = PackageSpawns[_index].Color;
        var nextState = PackageSpawns[_index].State;

        if (!(_elapsed > nextSpawnTime)) return;

        _elapsed -= nextSpawnTime;
            
        var position = transform.position + transform.up * 2;

        var package = Instantiate(Package, position, Quaternion.identity);

        package.GetComponent<Package>().Color = nextColor;
        package.GetComponent<Package>().State = nextState;
        package.name = string.Format("Package {0}", _index);
        
        package.gameObject.transform.SetParent(transform);

        _index++;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("package"))
            return;
        
        collision.rigidbody.velocity = Speed * transform.forward;
    }
}
