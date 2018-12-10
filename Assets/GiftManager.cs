﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviour {

	public List<GameObject> prefabs;

	// Use this for initialization
	void Start () {
		// InvokeRepeating("SpawnGift", 20.0f, 20.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnGift() {
		var randomPrefab = prefabs[Random.Range(0, prefabs.Count)];
        
		var gift = Instantiate(randomPrefab, new Vector3(-10,0,0), Quaternion.identity);
		gift.transform.position = new Vector3(Random.Range(-2.4f,2.4f), 2.4f, 1.5f);

		// gift.transform.position = new Vector3(0, 1, 1.5f);


	}
}
