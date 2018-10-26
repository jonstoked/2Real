using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DanceController : MonoBehaviour
{
    private int playerIndex = 0;

    [Tooltip("How much the player is jigglin'")]
    public float totalJointVelocity = 0;

    public float headVelocity = 0; //head is joint 3
    
    private AvatarController avatarController;
    private int jointTypeCount;
    private Vector3[] previousJointPositions;
    private long lastDepthFrameTime;

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

        if (lastDepthFrameTime != manager.GetSensorData().lastDepthFrameTime) //only check joint velocity when depth data updates
        {
            lastDepthFrameTime = manager.GetSensorData().lastDepthFrameTime;
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

                            if (i < previousJointPositions.Length)
                            {
                                //previous value exists, calculate velocity and add it to jiggle
                                Vector3 previousJointPos = previousJointPositions[i];
                                var jointDistanceDifference = (jointPos - previousJointPos).magnitude;
                                var jointVelocity = jointDistanceDifference / Time.deltaTime;
                                totalJointVelocity = totalJointVelocity + jointVelocity;

                                if (i == 3)
                                {
                                    headVelocity = jointVelocity;
                                }
                            }
                            previousJointPositions[i] = jointPos;
                        }
                    }


                }
            }
        }
    }
}

