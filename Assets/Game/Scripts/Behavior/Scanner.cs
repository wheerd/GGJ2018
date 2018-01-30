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
           gameObject.GetComponent<Package>().SetState(PackageState.Normal);
            _playMusicClipSignal.Fire(_packageScanSound);
        }
    }
}
