using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [Inject] private LevelModel _levelModel;

    [Inject] private PlayMusicClipSignal _playMusicClipSignal;

    [SerializeField] private AudioClip[] _spawnSounds;

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
            case "4": return PackageColor.Yellow;
            case "5": return PackageColor.Pink;
            case "6": return PackageColor.Orange;
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
            _levelModel.ExpectedPackageCount = runtimeSequence.Length;
        }
        else
        {
            _levelModel.ExpectedPackageCount = PackageSpawns.Count;
        }
    }

    private void AnimateTexture()
    {
        var material = GetComponent<MeshRenderer>().material;
        var offset = material.GetTextureOffset("_MainTex");
        offset.y -= Time.deltaTime * Speed / 2;
        material.SetTextureOffset("_MainTex", offset);
    }

    private void FixedUpdate ()
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

        CreatePackage(position, nextColor, nextState);

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

        CreatePackage(position, nextColor, nextState);

        _index++;
    }

    private void CreatePackage(Vector3 position, PackageColor nextColor, PackageState nextState)
    {
        if (nextState == PackageState.None)
        {
            return; // skip spawning, create gap
        }

        var package = Instantiate(Package, position, Package.transform.rotation);
        PlayPackageCreateSound();

        package.GetComponent<Package>().Color = nextColor;
        package.GetComponent<Package>().State = nextState;
        package.name = string.Format("Package {0}", _index);
        
        package.gameObject.transform.SetParent(transform.parent);
    }

    private void PlayPackageCreateSound()
    {
        var index = UnityEngine.Random.Range(0, _spawnSounds.Length);
        var sound = _spawnSounds[index];
        _playMusicClipSignal.Fire(sound);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("package"))
            return;
        
        collision.rigidbody.velocity = Speed * transform.forward;
    }
}
