using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviour {

	public List<GameObject> prefabs;

	// Use this for initialization
	void Start () {
		float giftInterval = 5f; //45f
		// InvokeRepeating("SpawnGift", giftInterval, giftInterval);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnGift() {
		var randomPrefab = prefabs[Random.Range(0, prefabs.Count)];
        
		var gift = Instantiate(randomPrefab, new Vector3(-10,0,0), Quaternion.identity);
		gift.transform.position = new Vector3(Random.Range(-2.4f,2.4f), 2.4f, 1.5f);
        var size = Random.Range(0.2f, 0.75f);
        gift.transform.localScale = new Vector3(size, size, size);

        // gift.transform.position = new Vector3(0, 1, 1.5f);


    }
}
