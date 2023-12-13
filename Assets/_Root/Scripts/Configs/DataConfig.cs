using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "DataConfig", menuName = "SavaMetaOcean/DataConfig", order = 0)]
public class DataConfig : ScriptableObject
{
    public NumberPlayer NumberPlayer;
    public NetworkRunner Runner;
    public CameraFollower CameraFollower;
    public PlayerConfig Player;
    public FishConfig Fish;
}

[Serializable]
public class PlayerConfig
{
    public GameObject Object;
    public float SpeedMove;
    public float SpeedRotate;
}

[Serializable]
public class FishConfig
{
    public List<Fish> Objects;
    public float SpeedMove;
    public float SpeedRotate;
}

public enum NumberPlayer
{
    Client = -1,
    Server = 0,
    ServerAndOnePlayer = 1,
    ServerAndTwoPlayer = 2
}
