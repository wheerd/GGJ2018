using UnityEngine;
using UnityEngine.AI;
using System;

namespace de.deichkrieger.characterControls
{
	[Serializable]
	public class ControllerAnimationTriggerMapping
	{
		public string Walk;
		public string Run;
		public string SlowDown;
		public string Stop;
	}

	[RequireComponent (typeof(NavMeshAgent))]
	public class ClickToMoveController : MonoBehaviour
	{
		public void OnDestroy ()
		{
			if (_targetMarkerInstance != null) {
				_targetMarkerInstance.SetActive (false);
				Destroy (_targetMarkerInstance.gameObject);
			}
		}

		[SerializeField]
		GameObject _targetMarker;
		GameObject _targetMarkerInstance;

		[SerializeField]
		bool _removeTargetMarkerCollided = true;

		[SerializeField]
		Camera _camera;

		[SerializeField]
		ControllerAnimationTriggerMapping TriggerMapping;

		[SerializeField]
		Animator anim;

		private NavMeshAgent navMeshAgent;
		private Ray shootRay;
		private RaycastHit shootHit;

		// Use this for initialization
		void Awake ()
		{
			navMeshAgent = GetComponent<NavMeshAgent> ();

			_targetMarkerInstance = Instantiate (_targetMarker);
			_targetMarkerInstance.SetActive (false);
		}

		private void OnTriggerEnter (Collider other)
		{
			if (_removeTargetMarkerCollided && other.gameObject == _targetMarkerInstance.gameObject) {
				_targetMarkerInstance.SetActive (false);
			}
		}

		private bool HasStopped ()
		{
			return (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
			&& (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon);
		}
        
		// Update is called once per frame
		void Update ()
		{
			Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
			RaycastHit interactionInfo;
			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (ray, out interactionInfo, 100)) {
					GameObject interactedObject = interactionInfo.collider.gameObject;
					InteractableObjectInterface interaction = interactedObject.GetComponent<InteractableObjectInterface> ();

					if (interaction != null) {
						interaction.HandleInteraction ();
					} else {
						navMeshAgent.destination = interactionInfo.point;
						navMeshAgent.isStopped = false;

						_targetMarkerInstance.transform.position = interactionInfo.point + new Vector3 (0f, 0.1f, 0f);
						_targetMarkerInstance.SetActive (true);
					}

				}
			}

			if (!HasStopped ()) {
				anim.SetTrigger (TriggerMapping.Walk);
			} else {
				anim.SetTrigger (TriggerMapping.Stop);
			}
		}
	}
}
