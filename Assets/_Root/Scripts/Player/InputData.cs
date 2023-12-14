using Fusion;
using UnityEngine;

public enum Buttons
{
    GripButtonLeft = 0,
    GripButtonRight = 1,
    TriggerButtonRight = 2
}

public struct InputData : INetworkInput
{
    public int MoveBody;
    public int RotateBody;
    public NetworkButtons GripButtonLeft;
    public NetworkButtons GripButtonRight;
    public Vector3 PositionHead;
    public Quaternion RotationHead;
    public Vector3 PositionRightHand;
    public Quaternion RotationRightHand;
    public Vector3 PositionLeftHand;
    public Quaternion RotationLeftHand;
    public NetworkButtons TriggerButtonRight;
}