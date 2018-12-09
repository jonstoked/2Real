using UnityEngine;
using System.Collections;

namespace SantaClaus
{
	public class RotateCamera : MonoBehaviour
	{

		public float rotationSpeed = 10f;
	
		void Update ()
		{
			if (Input.GetKey (KeyCode.A))
				transform.RotateAround (Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
			if (Input.GetKey (KeyCode.D))
				transform.RotateAround (Vector3.zero, Vector3.up, -rotationSpeed * Time.deltaTime);

			transform.LookAt (new Vector3 (0, 1f, 0));
		}
	}
}
