using System.Collections;
using UnityEngine;

public class LevelTutorialOverlay : MonoBehaviour
{

	[SerializeField] private GameObject _overlayToHide;

	private float _timeScale;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(Pause());
	}

	public void CloseOverlay()
	{
		Debug.Log ("Resume game");
		Time.timeScale = _timeScale;
		
		_overlayToHide.SetActive(false);
	}
	
	IEnumerator Pause()
	{
		yield return new WaitForSeconds(0.3f);
		
		Debug.Log ("Pause game");

		_timeScale = Time.timeScale;
		Time.timeScale = 0;
	}
}
