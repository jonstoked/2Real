using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class AvatarManager : MonoBehaviour {

	public List<GameObject> santas;
	public List<GameObject> ladies;
    public GameObject plane;
	public GameObject backgroundCamera1;
    public GameObject mainCamera;
    private KinectManager kinectManager;

    private bool tripping = false;

    void Start () {
		kinectManager = GameObject.Find("KinectController").GetComponent<KinectManager>();
    }
	
	void Update () {
		CheckForGroupPose();
        if(Input.GetKeyDown(KeyCode.A) && tripping == false)
        {
            Trip();
        }
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

	void CheckForGroupPose() {
		int userCount = kinectManager.GetUsersCount();
		int posedCount = 0;
		foreach(AvatarController ac in kinectManager.avatarControllers) {
			DanceController danceController = ac.gameObject.GetComponent<DanceController>();
			if (danceController.enabled && danceController.oneHandUp) {
				posedCount++;
			}
		}
		Debug.Log("p " + posedCount);
		Debug.Log(userCount);
		if(posedCount == userCount && userCount > 0 && tripping == false) {
            tripping = true;
            Trip();
		}
	}


    public void TakePhoto()
    {
        var keyCapture = GameObject.Find("KeyCommander").GetComponent<KeyCapture>();
        keyCapture.showCamera(false);
        keyCapture.TakePhoto();
    }

    void ShowCamera()
    {
        var keyCapture = GameObject.Find("KeyCommander").GetComponent<KeyCapture>();
        keyCapture.showCamera(true);
    }

    void Trip()
    {
        	Camera camera = backgroundCamera1.GetComponent<Camera>();
        	camera.enabled = false;
            Invoke("ShowCamera", 6);
            Invoke("UnTrip", 10);
    }

	void UnTrip() {
		Camera camera = backgroundCamera1.GetComponent<Camera>();
        camera.enabled = true;
        TakePhoto();
        tripping = false;
    }
}
