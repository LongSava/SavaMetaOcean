using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "DataConfig", menuName = "SavaMetaOcean/DataConfig", order = 0)]
public class DataConfig : ScriptableObject
{
    public NumberPlayer NumberPlayer;
    public NetworkRunner Runner;
    public Player Player;
}

public enum NumberPlayer
{
    Client = -1,
    Server = 0,
    ServerAndOnePlayer = 1,
    ServerAndTwoPlayer = 2
}
