using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
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

    public bool SimpleSequenzer = false;
    public string SimplePaketSequence;
    public float SimpleSpawnOffset = 1.0f;

    public float Speed = 5;

    public GameObject Package;

    public List<PackageSpawn> PackageSpawns = new List<PackageSpawn>();

    private PackageColor NumberToColor(string color)
    {
        Debug.Log("determine color " + color);
        switch (color)
        {
            case "1": return PackageColor.Red;
            case "2": return PackageColor.Blue;
            case "3": return PackageColor.Green;
            default: return PackageColor.Green;
        }
    }
    
    private PackageState NumberToState(string color)
    {
        Debug.Log("determine state " + color);
        if (color.Length == 2)
        {
            return PackageState.Blank;
        }
        
        switch (color)
        {
            case "0": return PackageState.None;
            default: return PackageState.Normal;
        }
    }
    
    private void Start ()
    {
        _index = 0;
        _elapsed = 0;

        if (SimpleSequenzer)
        {
            runtimeSequence = SimplePaketSequence.Split(',');
        }
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

        if (SimpleSequenzer)
        {
            SimpleSpawner();
        }
        else
        {
            ComplexSpawner();
        }

    }

    private string[] runtimeSequence;
    
    private void SimpleSpawner()
    {
        _elapsed += Time.deltaTime;

        if (_index >= runtimeSequence.Length)
        {
            return;
        }

        var nextPaket = runtimeSequence[_index];

        Debug.Log("next paket " + nextPaket);        
        
        var nextSpawnTime = SimpleSpawnOffset;
        var nextState = NumberToState(nextPaket);

        PackageColor nextColor;
        if (nextState == PackageState.Blank)
        {
            nextColor = NumberToColor(nextPaket.Substring(0,1));
        }
        else
        {
            nextColor = NumberToColor(nextPaket);
        }


        if (!(_elapsed > nextSpawnTime)) return;
        
        _elapsed -= nextSpawnTime;
        
        var position = transform.position + transform.up * 2;

        CreatePaket(position, nextColor, nextState);

        _index++;
    }
    
    private void ComplexSpawner()
    {
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

        CreatePaket(position, nextColor, nextState);

        _index++;
    }

    private void CreatePaket(Vector3 position, PackageColor nextColor, PackageState nextState)
    {
        if (nextState == PackageState.None)
        {
            return; // skip spawning, create gap
        }
        
        var package = Instantiate(Package, position, Quaternion.identity);

        package.GetComponent<Package>().Color = nextColor;
        package.GetComponent<Package>().State = nextState;
        package.name = string.Format("Package {0}", _index);
        
        package.gameObject.transform.SetParent(transform);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("package"))
            return;
        
        collision.rigidbody.velocity = Speed * transform.forward;
    }
}
