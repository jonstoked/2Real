using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBehavior : MonoBehaviour {

	Rigidbody body;
	float horizontalSpinAmount;
	float verticalSpinAmount;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		horizontalSpinAmount = Random.Range(-10.0f,10.0f);
		verticalSpinAmount = Random.Range(-10.0f,10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate ()
    {
        float h = horizontalSpinAmount * Time.deltaTime;
        float v = verticalSpinAmount * Time.deltaTime;
        
        body.AddTorque(transform.up * h);
		body.AddTorque(transform.right * v);
    }
}
