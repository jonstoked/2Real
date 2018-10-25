using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DanceController : MonoBehaviour
{

    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    private int playerIndex = 0;

    //[Tooltip("The Kinect joint we want to track.")]
    //public KinectInterop.JointType joint = KinectInterop.JointType.HandRight;

    [Tooltip("Current joint position in Kinect coordinates (meters).")]
    public Vector3 jointPosition;

    [Tooltip("How much the player is jigglin'")]
    public float jiggle = 0;

    private AvatarController avatarController;

    private void Awake()
    {
        avatarController = GetComponent<AvatarController>();
        //Debug.Log("avatar controller index: " + avatarController.playerIndex);
        playerIndex = avatarController.playerIndex;
    }

    void Update()
    {
        // get the joint position
        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized())
        {
            if (manager.IsUserDetected(playerIndex))
            {
                long userId = manager.GetUserIdByIndex(playerIndex);

                var jointTypes = Enum.GetValues(typeof(KinectInterop.JointType));
                for (int i = 0; i < jointTypes.Length; ++i)
                {
                    //var joint = KinectInterop.JointType.HandRight;
                    //if (manager.IsJointTracked(userId, (int)joint))
                    if (manager.IsJointTracked(userId, i))
                    {
                        Vector3 jointPos = manager.GetJointPosition(userId, i);
                        if (i==0)
                        {
                            Debug.Log(jointPos);
                        }

                    }
                }


            }
        }

    }


}

