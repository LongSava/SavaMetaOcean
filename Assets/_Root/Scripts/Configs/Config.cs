using UnityEngine;

public static class Config
{
    public static DataConfig Data;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        Data = Resources.Load<DataConfig>("DataConfig");
    }
}
