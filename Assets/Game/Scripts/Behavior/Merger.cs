using Assets.Game.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Merger : MonoBehaviour
{
    public float TimeBefore;

    private int index;

    private float elapsed;

    public float OutputSpeed = 5.0f;

    private readonly Queue<GameObject> packageQueue = new Queue<GameObject>();

    private readonly HashSet<GameObject> ignored = new HashSet<GameObject>();

    [Inject] private PlayMusicClipSignal playMusicClipSignal;

    [SerializeField] private AudioClip packageMergedSound;

    private void Start ()
    {
        
    }

    private void Update ()
    {
        if (!packageQueue.Any())
        {
            return;
        }

        elapsed += Time.deltaTime;

        var nextSpawnTime = TimeBefore;

        if (!(elapsed > nextSpawnTime)) return;

        elapsed -= nextSpawnTime;

        var package = packageQueue.Dequeue();
        MovePackageToExit(package);
    }

    private void MovePackageToExit(GameObject gameObject)
    {
        gameObject.layer = Layers.Default;

        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.velocity = transform.forward * OutputSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var gameObject = other.gameObject;
        if (gameObject.CompareTag(Tags.Package))
        {
            if (ignored.Contains(other.gameObject)) return;
            ignored.Add(other.gameObject);

            var position = transform.position;
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.isKinematic = true;
            rigidBody.velocity = Vector3.zero;
            rigidBody.MovePosition(position);

            gameObject.layer = Layers.DisabledPackages;

            playMusicClipSignal.Fire(packageMergedSound);

            packageQueue.Enqueue(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Package))
        {
            ignored.Remove(other.gameObject);
        }
    }
}
