using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

    private FishSpawner m_Instance;
    public FishSpawner Instance { get { return m_Instance; } }
    public List<GameObject> fishPrefabs;
    public List<GameObject> fishes;

    void Awake()
    {
        m_Instance = this;
        InvokeRepeating("SpawnRandomFish", 3.0f, 3.0f);
    }

    void OnDestroy()
    {
        m_Instance = null;  
    }

    void Update()
    {
       
    }

    void SpawnRandomFish()
    {
        if(fishes.Count >= 25) {
            return;
        }
        var randomFishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Count)];
        var fish = Instantiate(randomFishPrefab, new Vector3(-10,0,0), Quaternion.identity);
        fishes.Add(fish);
        fish.transform.localScale = new Vector3(.5f*fish.transform.localScale.x, .5f*fish.transform.localScale.y, .5f*fish.transform.localScale.z);
        var fishMovementScript = fish.GetComponent<FishMovement>();

        // PositionFishNextToRightHand(fish, fishMovementScript);
        // return;

        //set position and direction of fish
        if (Random.Range(0, 2) == 0)
        {
            fish.transform.position = new Vector3(-2.4f, Random.Range(-2.4f,2.4f), 1.5f);
            fishMovementScript.direction = Vector3.right;
            fishMovementScript.flipXDirection();
        }
        else
        {
            fish.transform.position = new Vector3(2.4f, Random.Range(-2.4f, 2.4f), 1.5f);
            fishMovementScript.direction = Vector3.left;
        }

    }

    void PositionFishNextToRightHand(GameObject fish, FishMovement script) {
        fish.transform.position = new Vector3(0.5f, 1, 3f);
        script.direction = Vector3.left;
    }

   }
