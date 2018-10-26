using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DanceController : MonoBehaviour
{
    public float totalJointVelocity = 0;
    public float headVelocity = 0; //head is joint 3
    public bool handsUp = false;

    private int playerIndex = 0;
    private AvatarController avatarController;
    private int jointTypeCount;
    private Vector3[] previousJointPositions;
    private long lastDepthFrameTime;

    private KinectManager manager;
    private long userId; //I believe this needs to be updated each frame.  I don't think it always correlates with playerIndex

    private void Awake()
    {
        avatarController = GetComponent<AvatarController>();
        playerIndex = avatarController.playerIndex;
        jointTypeCount = Enum.GetValues(typeof(KinectInterop.JointType)).Length;
        previousJointPositions = new Vector3[jointTypeCount];
    }

    void Update()
    {
        manager = KinectManager.Instance;
        userId = manager.GetUserIdByIndex(playerIndex);

        if (lastDepthFrameTime != manager.GetSensorData().lastDepthFrameTime) //only check joint velocity when depth data updates
        {
            lastDepthFrameTime = manager.GetSensorData().lastDepthFrameTime;
            if (manager && manager.IsInitialized())
            {
                if (manager.IsUserDetected(playerIndex))
                {
                    CalculateTotalJointVelocity();
                    CheckForHandsUpPose();

                    


                }
            }
        }
    }

   /* public enum JointType : int
    {
        SpineBase = 0,
        SpineMid = 1,
        Neck = 2,
        Head = 3,
        ShoulderLeft = 4,
        ElbowLeft = 5,
        WristLeft = 6,
        HandLeft = 7,
        ShoulderRight = 8,
        ElbowRight = 9,
        WristRight = 10,
        HandRight = 11,
        HipLeft = 12,
        KneeLeft = 13,
        AnkleLeft = 14,
        FootLeft = 15,
        HipRight = 16,
        KneeRight = 17,
        AnkleRight = 18,
        FootRight = 19,
        SpineShoulder = 20,
        HandTipLeft = 21,
        ThumbLeft = 22,
        HandTipRight = 23,
        ThumbRight = 24
        //Count = 25
    }*/

    private void CheckForHandsUpPose()
    {
        var leftHandPosition = JointPos(7);
        var rightHandPosition = JointPos(11);

        var leftShoulderPosition = JointPos(4);
        var rightShoulderPosition = JointPos(8);

        float diffFactor = 1.3f;

        handsUp = (leftHandPosition.y > leftShoulderPosition.y * diffFactor && rightHandPosition.y > rightShoulderPosition.y * diffFactor);
    }

    Vector3 JointPos(int jointIndex)
    {
        return manager.GetJointPosition(userId, jointIndex);
    }

    void CalculateTotalJointVelocity()
    {
        //sum velocity of each joint for this frame
        totalJointVelocity = 0;

        for (int i = 0; i < jointTypeCount; ++i)
        {
            if (manager.IsJointTracked(userId, i))
            {
                Vector3 jointPos = manager.GetJointPosition(userId, i);

                if (i < previousJointPositions.Length)
                {
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

