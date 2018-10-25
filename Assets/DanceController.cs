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
    public float totalJointVelocity = 0;

    public float headVelocity = 0; //head is joint 3
    public Vector3 headPosition;

    private AvatarController avatarController;
    private int jointTypeCount;
    private Vector3[] previousJointPositions;
    

    private void Awake()
    {
        avatarController = GetComponent<AvatarController>();
        playerIndex = avatarController.playerIndex;
        jointTypeCount = Enum.GetValues(typeof(KinectInterop.JointType)).Length;
        previousJointPositions = new Vector3[jointTypeCount];
    }

    void Update()
    {
        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized())
        {
            if (manager.IsUserDetected(playerIndex))
            {
                long userId = manager.GetUserIdByIndex(playerIndex);

                //get instantaneous velocity of each joint
                totalJointVelocity = 0;
                for (int i = 0; i < jointTypeCount; ++i)
                {
                    if (manager.IsJointTracked(userId, i))
                    {
                        Vector3 jointPos = manager.GetJointPosition(userId, i);

                        if (i == 3)
                        {
                            headPosition = jointPos;
                        }

                        if (i < previousJointPositions.Length)
                        {
                            //previous value exists, calculate velocity and add it to jiggle
                            Vector3 previousJointPos = previousJointPositions[i];
                            var jointVelocity = (jointPos - previousJointPos).magnitude / Time.deltaTime;
                            totalJointVelocity = totalJointVelocity + jointVelocity;

                            if(i==3)
                            {
                                headVelocity = jointVelocity;
                                headPosition = jointPos;
                            }
                        }
                        previousJointPositions[i] = jointPos;
                    }
                }


            }
        }

    }


}

