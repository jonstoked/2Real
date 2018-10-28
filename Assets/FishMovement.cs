using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour {

    private Bounds bounds = new Bounds(new Vector3(0, 0, 2), new Vector3(4, 3, 2));
    private float speed = 10f;
    private Vector3 direction;

	// Use this for initialization
	void Start () {
        direction = Vector3.left;
    }

    // Update is called once per frame
    void Update () {
        direction = Quaternion.Euler(0, 5f, 0) * direction;

        transform.Translate(speed * direction * Time.deltaTime, Space.World);
    }
}
