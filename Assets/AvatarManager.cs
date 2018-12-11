using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour {

	public List<GameObject> santas;

	public List<GameObject> ladies;
	private KinectManager kinectManager;

	void Start () {
		kinectManager = GameObject.Find("KinectController").GetComponent<KinectManager>();
	}
	
	void Update () {
		
	}

	public void SwapAvatarAtIndex(int playerIndex) {
		var r = ladies[playerIndex].GetComponentInChildren<SkinnedMeshRenderer>();
			bool isLady = r.enabled;
		if(isLady) {
			showSantaAtIndex(playerIndex,true);
			showLadyAtIndex(playerIndex,false);
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
		var lady = ladies[playerIndex];
		lady.GetComponentInChildren<DanceController>().enabled = !show;
	}

	public void showLadyAtIndex(int playerIndex, bool show) {
		var lady = ladies[playerIndex];
		lady.GetComponentInChildren<SkinnedMeshRenderer>().enabled = show;
		var santa = santas[playerIndex];
		santa.GetComponentInChildren<DanceController>().enabled = !show;
	}

	// public void makeLady(int playerIndex) {
	// 	DestroyPlayerAtIndex(playerIndex);

	// 	var nextAvatar = Instantiate(ladyPrefab, new Vector3(-10,0,0), Quaternion.identity);
	// 	var nextAvatarController = nextAvatar.GetComponent<AvatarController>();
	// 	nextAvatarController.playerIndex = playerIndex;
	// 	nextAvatarController.posRelativeToCamera = Camera.main;
		
	// 	kinectManager.avatarControllers.Add(nextAvatarController);
	// 	kinectManager.RefreshAvatarUserIds();
	// }

	// public void makeSanta(int playerIndex) {
	// 	DestroyPlayerAtIndex(playerIndex);

	// 	var nextAvatar = Instantiate(santaPrefab, new Vector3(-10,0,0), Quaternion.identity);
	// 	nextAvatar.transform.Rotate(0,180,0);

	// 	var nextAvatarController = nextAvatar.GetComponent<AvatarController>();
	// 	nextAvatarController.playerIndex = playerIndex;
	// 	nextAvatarController.posRelativeToCamera = Camera.main;
		
	// 	nextAvatar.GetComponent<DanceController>().isSanta = true;
	// 	var dc = nextAvatar.GetComponent<DanceController>();
	// 	kinectManager.avatarControllers.Add(nextAvatarController);
	// 	kinectManager.RefreshAvatarUserIds();
	// }

	// private void DestroyPlayerAtIndex(int playerIndex) {
	// 	AvatarController currentAvatarController = null;
	// 	foreach (AvatarController ac in kinectManager.avatarControllers) {
	// 		if (ac.playerIndex == playerIndex) {
	// 			currentAvatarController = ac;
	// 		}
	// 	}
	// 	if(!currentAvatarController) return;

	// 	kinectManager.avatarControllers.Remove(currentAvatarController);
	// 	//Destroy(currentAvatarController.gameObject);
	// 	currentAvatarController.gameObject.SetActive(false);
	// }


}
