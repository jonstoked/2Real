using UnityEngine;
using System.Collections;

namespace SantaClaus
{
	public class RotateLights : MonoBehaviour
	{

		public	Vector3 rotateDir;
		public float rotationSpeed = 5f;

		void Update ()
		{
			if (Input.GetKey (KeyCode.Q))
				transform.Rotate (rotateDir * Time.deltaTime);
			if (Input.GetKey (KeyCode.E))
				transform.Rotate (-rotateDir * Time.deltaTime);
		}
	}
}