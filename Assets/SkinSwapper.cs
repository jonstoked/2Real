using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSwapper : MonoBehaviour {

    public Material[] materials;
    Renderer rend;
    int currentSkin = 0;

	void Start () {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = materials[currentSkin];
        InvokeRepeating("SwapSkin", 3.0f, 3.0f);
	}
	
	void Update () {
        ScrollMaterial();
	}

    void ScrollMaterial ()
    {
        var offset = Time.time * 0.1f;
        rend.sharedMaterial.mainTextureOffset = new Vector2(offset, 0); 
    }

    void SwapSkin ()
    {
        ++currentSkin;
        if (currentSkin >= materials.Length) {
            currentSkin = 0;
        }
        rend.sharedMaterial = materials[currentSkin];
    }
}
