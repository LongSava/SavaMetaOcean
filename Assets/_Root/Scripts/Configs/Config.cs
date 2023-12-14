using UnityEngine;

public static class Config
{
    public static DataConfig Data;
    public static AudioConfig Audio;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        Data = Resources.Load<DataConfig>("DataConfig");
        Audio = Resources.Load<AudioConfig>("AudioConfig");
    }
}
