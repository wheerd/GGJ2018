using UnityEngine;

namespace de.deichkrieger.characterControls
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField]
		Camera _camera;

		[SerializeField]
		bool _freezeX = false;

		[SerializeField]
		bool _freezeY = false;

		[SerializeField]
		bool _freezeZ = false;

		void Update ()
		{
			float newX = _freezeX ? _camera.transform.position.x : gameObject.transform.position.x;
			float newY = _freezeY ? _camera.transform.position.y : gameObject.transform.position.y;
			float newZ = _freezeZ ? _camera.transform.position.z : gameObject.transform.position.z;


			_camera.transform.position = new Vector3 (newX, newY, newZ);
		}
	}
}