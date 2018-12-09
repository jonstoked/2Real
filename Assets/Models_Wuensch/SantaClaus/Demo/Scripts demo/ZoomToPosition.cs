using UnityEngine;
using System.Collections;

namespace SantaClaus
{
	public class ZoomToPosition : MonoBehaviour
	{

		public Transform zoomMax;
		public Transform zoomMin;

		// Update is called once per frame
		void Update ()
		{
			if (Input.GetKey (KeyCode.W)) {
				transform.position = Vector3.Slerp (transform.position, zoomMax.position, Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.position = Vector3.Slerp (transform.position, zoomMin.position, Time.deltaTime);
			}
		}
	}
}
