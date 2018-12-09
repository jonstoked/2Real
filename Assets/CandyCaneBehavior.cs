using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating("Remove", 6.0f, 6.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Remove() {
		Destroy(gameObject);
	}
}
