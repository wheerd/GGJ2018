using UnityEngine;
using System;

namespace de.deichkrieger.characterControls
{
	[Serializable]
	public class ControllerInputMapping
	{
		public string Horizontal;
		public string Vertical;
	}

	public class FirstPersonController : MonoBehaviour
	{
		[SerializeField]
		float _moveSpeed;

		[SerializeField]
		float _rotateDegreesPerSeond;

		[SerializeField]
		Animator _animator;

		[SerializeField]
		ControllerAnimationTriggerMapping _triggerMapping;

		[SerializeField]
		ControllerInputMapping _inputMapping;
	
		// Update is called once per frame
		void Update ()
		{
			float translation = Input.GetAxis (_inputMapping.Vertical) * _moveSpeed;
			float turn = Input.GetAxis (_inputMapping.Horizontal) * _moveSpeed;

			translation *= Time.deltaTime;
			gameObject.transform.Translate (0, 0, translation);

			if (turn != 0.0f) {
				var leftOrRight = (turn > 0) ? 1 : -1;
				Transform t = this.gameObject.GetComponent<Transform> ();
				t.Rotate (new Vector3 (0, leftOrRight * _rotateDegreesPerSeond * Time.deltaTime, 0)); 
			}

			if (translation != 0.0f) {
				_animator.SetTrigger (_triggerMapping.Walk);
			} else {
				_animator.SetTrigger (_triggerMapping.Stop);
			}

		}
	}
}