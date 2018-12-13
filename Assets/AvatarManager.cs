using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AvatarManager : MonoBehaviour {

	public List<GameObject> santas;
	public List<GameObject> ladies;
    public GameObject plane;
	public GameObject backgroundCamera1;
	private KinectManager kinectManager;
    WebCamTexture webCamTexture;
    int photoNumber = 0;

    private bool tripping = false;
	

	void Start () {
		kinectManager = GameObject.Find("KinectController").GetComponent<KinectManager>();
        webCamTexture = new WebCamTexture();
        webCamTexture.deviceName = "Kinect V2 Video Sensor";
        plane.GetComponentInChildren<Renderer>().material.mainTexture = webCamTexture;
        plane.GetComponentInChildren<Renderer>().enabled = false;
        webCamTexture.Play();
    }
	
	void Update () {
		CheckForStrongMen();
	}

	public void SwapAvatarAtIndex(int playerIndex) {
		var r = ladies[playerIndex].GetComponentInChildren<SkinnedMeshRenderer>();
		bool isLady = r.enabled;
		if(isLady) {
			showLadyAtIndex(playerIndex,false);
			showSantaAtIndex(playerIndex,true);
		} else {
			showSantaAtIndex(playerIndex,false);
			showLadyAtIndex(playerIndex,true);
		}
	}

	public void showSantaAtIndex(int playerIndex, bool show) {
		var santa = santas[playerIndex];
		foreach (SkinnedMeshRenderer r in santa.GetComponentsInChildren<SkinnedMeshRenderer>()) {
			r.enabled = show;
		}
		var danceController = santa.GetComponentInChildren<DanceController>();
		if (show) {
			kinectManager.gestureListeners.Add(danceController);
		} else {
			kinectManager.gestureListeners.Remove(danceController);
		}
		danceController.enabled = show;
	}

	public void showLadyAtIndex(int playerIndex, bool show) {
		var lady = ladies[playerIndex];
		lady.GetComponentInChildren<SkinnedMeshRenderer>().enabled = show;
		var danceController = lady.GetComponentInChildren<DanceController>();
		if (show) {
			kinectManager.gestureListeners.Add(danceController);
		} else {
			kinectManager.gestureListeners.Remove(danceController);
		}

		danceController.enabled = show;
	}

	void CheckForStrongMen() {
		int userCount = kinectManager.GetUsersCount();
		int strongManCount = 0;
		foreach(AvatarController ac in kinectManager.avatarControllers) {
			DanceController danceController = ac.gameObject.GetComponent<DanceController>();
			if (danceController.enabled && danceController.strongMan) {
				strongManCount++;
			}
		}
		if(strongManCount == userCount && userCount > 0 && backgroundCamera1.GetComponent<Camera>().enabled == true) {
            PreparePhoto();
		}
	}


    void TakePhoto()
    {
        Debug.Log("PHOTOS: " + Application.persistentDataPath + "/photo" + photoNumber + ".png");

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        File.WriteAllBytes(Application.persistentDataPath + "/photo" + photoNumber + ".png", bytes);
        photoNumber++;
    }

    void PreparePhoto()
    {
        plane.GetComponentInChildren<Renderer>().enabled = true;
        Invoke("StopRenderer", 3);
    }

    void StopRenderer()
    {
        plane.GetComponentInChildren<Renderer>().enabled = false;
        Invoke("Trip", 1);
    }

    void Trip()
    {
        
        if (!tripping) {
            tripping = true;
        	Camera camera = backgroundCamera1.GetComponent<Camera>();
        	camera.enabled = false;
            Invoke("TakePhoto", 4);
            Invoke("UnTrip", 10);
		}
    }

	void UnTrip() {
		tripping = false;
		Camera camera = backgroundCamera1.GetComponent<Camera>();
        camera.enabled = true;
	}
}
