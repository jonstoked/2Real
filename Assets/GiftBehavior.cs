using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBehavior : MonoBehaviour {

	Rigidbody body;
	float horizontalSpinAmount;
	float verticalSpinAmount;
	public GameObject candyCanePrefab;

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

	private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Avatar" || collision.gameObject.tag == "Santa")
        {
            CandyCaneSplosion();
			Destroy(gameObject);            
        }
    }

	void CandyCaneSplosion() {
		for (int i = 0; i < 40; ++i) {
        	var cane = Instantiate(candyCanePrefab, transform.localPosition, Quaternion.identity);
			var rb = cane.GetComponent<Rigidbody>();
			var f = 20;
			rb.AddForce(Random.Range(-f,f), Random.Range(0,2*f), Random.Range(-f,f), ForceMode.Impulse);
			var t = 5;
			rb.AddTorque(new Vector3(Random.Range(-t,t), Random.Range(-t,t), Random.Range(-t,t)), ForceMode.Impulse);
		}
	}
}
