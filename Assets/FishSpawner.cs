using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

    private FishSpawner m_Instance;
    public FishSpawner Instance { get { return m_Instance; } }
    public List<GameObject> fishPrefabs;
    private List<GameObject> fish;

    void Awake()
    {
        m_Instance = this;
        InvokeRepeating("SpawnRandomFish", 6.0f, 6.0f);

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
        var randomFishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Count)];
        var fish = Instantiate(randomFishPrefab, new Vector3(1, 1, 1.5f), Quaternion.identity);
        fish.transform.localScale = new Vector3(.5f*fish.transform.localScale.x, .5f*fish.transform.localScale.y, .5f*fish.transform.localScale.z);

    }

    Vector3 RandomPositionAlongVerticalEdge()
    {
        return new Vector3(0, 0, 0);
    }

   }
