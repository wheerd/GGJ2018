using Assets.Game.Scripts;
using UnityEngine;
using Zenject;

public class Scanner : MonoBehaviour
{
    [Inject] private PlayMusicClipSignal _playMusicClipSignal;
    [SerializeField] private AudioClip _packageScanSound;
    
    public float OutputSpeed = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        var gameObject = other.gameObject;
        if (gameObject.CompareTag(Tags.Package))
        {
            var position = transform.position;
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            //rigidBody.velocity = Vector3.zero;
            //rigidBody.MovePosition(position);
            //rigidBody.isKinematic = true;

            gameObject.GetComponent<Package>().SetState(PackageState.Normal);
            //MovePackageToExit(gameObject);

            _playMusicClipSignal.Fire(_packageScanSound);
        }
    }

      private void MovePackageToExit(GameObject gameObject)
    {
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.velocity = transform.forward * -1 * OutputSpeed;
    }
}
