using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DanceController : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    public float totalJointVelocity = 0;
    public float headVelocity = 0; //head is joint 3
    public float initialHeadScale = 1.0f;
    public bool handsUp = false;
    public bool chickenArms = false;
    public bool legUp = false;
    public bool squatting = false;
    public bool handface = false;

    public GameObject backgroundCamera1;

    private int playerIndex = 0;
    private AvatarController avatarController;
    private AvatarScaler avatarScaler;
    private Renderer renderer;
    private int jointTypeCount;
    private Vector3[] previousJointPositions;
    private long lastDepthFrameTime;

    private KinectManager manager;
    private long userId; //I believe this needs to be updated each frame.  I don't think it always correlates with playerIndex

    public float handToShoulderDistanceLeft;
    public float handToShoulderDistanceRight;

    public BoxCollider leftHandCollider;
    public BoxCollider rightHandCollider;
    public bool collidingWithFish = false;

    public Vector3 leftKnee;
    public Vector3 rightKnee;

    private void Awake()
    {
        avatarController = GetComponent<AvatarController>();
        avatarScaler = GetComponent<AvatarScaler>();
        renderer = GetComponentInChildren<Renderer>();
        playerIndex = avatarController.playerIndex;
        jointTypeCount = Enum.GetValues(typeof(KinectInterop.JointType)).Length;
        previousJointPositions = new Vector3[jointTypeCount];
        InvokeRepeating("ToggleCamera1", 3.0f, 3.0f);
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
                    CheckForHandsUp();
                    CheckForChickenArms();
                    checkForLegUp();
                    CheckForHandFace();
                    ScaleBodyParts();
                    PositionHandColliders();

                    


                }
            }
        }
    }

    void ToggleCamera1()
    {
        Camera camera = backgroundCamera1.GetComponent<Camera>();
        //camera.enabled = !camera.enabled;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            collidingWithFish = true;
            var fish = collision.gameObject;
            var skinnedMeshRenderer = fish.GetComponentInChildren<SkinnedMeshRenderer>();
            renderer.material = skinnedMeshRenderer.material;
            fish.GetComponent<FishMovement>().Remove();            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            collidingWithFish = false;
        }
    }

    private void PositionHandColliders()
    {
        //convert to global hand positions to local space
        //leftHandCollider.center = transform.InverseTransformPoint(JointPos(7));
        rightHandCollider.center = transform.InverseTransformPoint(JointPos(11));
        leftHandCollider.center = rightHandCollider.center;
    }

    public void UserDetected(long userId, int userIndex)
    {
        // the gestures are allowed for the primary user only
        KinectManager manager = KinectManager.Instance;

        // detect these user specific gestures
        manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        manager.DetectGesture(userId, KinectGestures.Gestures.Wave);
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

    private void CheckForHandsUp()
    {
        var leftHandPosition = JointPos(7);
        var rightHandPosition = JointPos(11);

        var leftShoulderPosition = JointPos(4);
        var rightShoulderPosition = JointPos(8);

        float diffFactor = 1.3f;

        handsUp = (leftHandPosition.y > leftShoulderPosition.y * diffFactor && rightHandPosition.y > rightShoulderPosition.y * diffFactor);
    }

    private void CheckForHandFace()
    {
        var leftHand = JointPos(7);
        var rightHand = JointPos(11);
        var head = JointPos(3);

        var headToHandDistanceLeft = (leftHand - head).magnitude;
        var headToHandDistanceRight = (rightHand - head).magnitude;

        handface = (headToHandDistanceLeft< 0.2f && headToHandDistanceRight< 0.2f);
    }

private void CheckForChickenArms()
    {
        var leftHand = JointPos(7);
        var rightHand = JointPos(11);
        var leftShoulder = JointPos(4);
        var rightShoulder = JointPos(8);

        handToShoulderDistanceLeft = (leftHand - leftShoulder).magnitude;
        handToShoulderDistanceRight = (rightHand - rightShoulder).magnitude;

        chickenArms = (handToShoulderDistanceLeft < 0.2f && handToShoulderDistanceRight < 0.2f);
    }

    private void checkForLegUp()
    {
        leftKnee = JointPos(13);
        rightKnee = JointPos(17);

        float diffFactor = 1.5f;

        legUp = (rightKnee.y > leftKnee.y * diffFactor || leftKnee.y > rightKnee.y * diffFactor);
    }

    private void CheckForSquat()
    {
        //leftFoot = JointPos(15);
        //rightFoot = JointPos(19);
        //leftFootToSpineBaseDistance = (JointPos())
    }

    private void ScaleBodyParts()
    {
        float scaleRate = .05f;
        if(handsUp)
        {
            avatarScaler.armScaleFactor += scaleRate;
        }

        if(chickenArms)
        {
            avatarScaler.armScaleFactor -= scaleRate;
        }

        if(squatting)
        {
            avatarScaler.legScaleFactor -= scaleRate;
        }

        if (handface)
        {
            avatarScaler.headScaleFactor += scaleRate;
        }
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

    public void UserLost(long userId, int userIndex)
    {
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (userIndex == this.playerIndex)
        {
            if (gesture == KinectGestures.Gestures.Wave)
            {
                avatarScaler.bodyScaleFactor = 1.05f;
                avatarScaler.armScaleFactor = 0.95f;
                avatarScaler.legScaleFactor = 0.95f;
                avatarScaler.headScaleFactor = initialHeadScale;
            }
            else if (gesture == KinectGestures.Gestures.Jump)
            {
                avatarScaler.bodyScaleFactor = UnityEngine.Random.Range(0f, 2.0f);
                avatarScaler.armScaleFactor = UnityEngine.Random.Range(0f, 2.0f);
                avatarScaler.legScaleFactor = UnityEngine.Random.Range(0f, 2.0f);
                avatarScaler.headScaleFactor = UnityEngine.Random.Range(0f, 2.0f);
            }
        }
        return true;
    }

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
    {
        return true;
    }
}

