using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour {

	public GameObject ladyPrefab;
	public GameObject santaPrefab;

	private KinectManager kinectManager;

	void Start () {
		kinectManager = GameObject.Find("KinectController").GetComponent<KinectManager>();
	}
	
	void Update () {
		
	}

	public void makeLady(int playerIndex) {
		DestroyPlayerAtIndex(playerIndex);

		var nextAvatar = Instantiate(ladyPrefab, new Vector3(-10,0,0), Quaternion.identity);
		var nextAvatarController = nextAvatar.GetComponent<AvatarController>();
		nextAvatarController.playerIndex = playerIndex;
		nextAvatarController.posRelativeToCamera = Camera.main;
		
		kinectManager.avatarControllers.Add(nextAvatarController);
		kinectManager.RefreshAvatarUserIds();
	}

	public void makeSanta(int playerIndex) {
		DestroyPlayerAtIndex(playerIndex);

		var nextAvatar = Instantiate(santaPrefab, new Vector3(-10,0,0), Quaternion.identity);
		var nextAvatarController = nextAvatar.GetComponent<AvatarController>();
		nextAvatarController.playerIndex = playerIndex;
		nextAvatarController.posRelativeToCamera = Camera.main;
		
		nextAvatar.GetComponent<DanceController>().isSanta = true;
		
		kinectManager.avatarControllers.Add(nextAvatarController);
		kinectManager.RefreshAvatarUserIds();
	}

	private void DestroyPlayerAtIndex(int playerIndex) {
		AvatarController currentAvatarController = null;
		foreach (AvatarController ac in kinectManager.avatarControllers) {
			if (ac.playerIndex == playerIndex) {
				currentAvatarController = ac;
			}
		}
		if(!currentAvatarController) return;

		kinectManager.avatarControllers.Remove(currentAvatarController);
		Destroy(currentAvatarController.gameObject);
	}


}
