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
        InvokeRepeating("SpawnRandomFish", 1.0f, 1.0f);

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
        Instantiate(randomFishPrefab, new Vector3(1, 1, 1.5f), Quaternion.identity);

    }

   }
