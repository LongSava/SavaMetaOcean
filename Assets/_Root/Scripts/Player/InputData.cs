using Fusion;
using UnityEngine;

public enum Buttons
{
    GrapLeft = 0,
    GrapRight = 1
}

public struct InputData : INetworkInput
{
    public int MoveBody;
    public int RotateBody;
    public NetworkButtons GrapLeftValue;
    public NetworkButtons GrapRightValue;
    public Vector3 PositionHead;
    public Quaternion RotationHead;
    public Vector3 PositionRightHand;
    public Quaternion RotationRightHand;
    public Vector3 PositionLeftHand;
    public Quaternion RotationLeftHand;
}