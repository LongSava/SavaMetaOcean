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
    public FishAreasConfig FishAreas;
    public float SpeedGyreLine;
}

[Serializable]
public class PlayerConfig
{
    public GameObject Object;
    public float SpeedMove;
    public float SpeedRotate;
    public Vector3 PositionSpawnedOcean;
    public Vector3 PositionSpawnedTitanic;
    public Vector3 PositionSpawnedTitanicToOcean;
}

[Serializable]
public class FishConfig
{
    public List<Fish> Objects;
    public float SpeedMove;
    public float SpeedRotate;
    public float RangeTargetPosition;
    public float RangeSpeedMove;
}

[Serializable]
public class FishAreasConfig
{
    public FishAreas Object;
    public List<FishAreaConfig> FishAreas;
}

[Serializable]
public class FishAreaConfig
{
    public List<FishFlockConfig> FishFlocks;
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Size;
    public int NumberFish;
}

[Serializable]
public class FishFlockConfig
{
    public float SpeedMove;
}

public enum NumberPlayer
{
    Client = -1,
    Server = 0,
    ServerAndOnePlayer = 1,
    ServerAndTwoPlayer = 2
}
