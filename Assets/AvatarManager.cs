using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour {

	public List<GameObject> santas;
	public List<GameObject> ladies;
	public GameObject backgroundCamera1;
	private KinectManager kinectManager;

	private bool tripping = false;
	

	void Start () {
		kinectManager = GameObject.Find("KinectController").GetComponent<KinectManager>();
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
		if(strongManCount == userCount) {
			Trip();
		}
	}

	void Trip()
    {
		if(!tripping) {
			tripping = true;
        	Camera camera = backgroundCamera1.GetComponent<Camera>();
        	camera.enabled = false;
			Invoke("UnTrip", 10);
		}
    }

	void UnTrip() {
		tripping = false;
		Camera camera = backgroundCamera1.GetComponent<Camera>();
        camera.enabled = true;
	}

}
