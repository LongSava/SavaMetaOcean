using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class FishArea : MonoBehaviour
{
    public FishAreaConfig Config;
    public List<FishFlock> FishFlocks = new List<FishFlock>();
    public float XHalf;
    public float YHalf;
    public float ZHalf;

    public void Init(FishAreaConfig config, NetworkRunner runner)
    {
        Config = config;

        transform.SetPositionAndRotation(config.Position, config.Rotation);

        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.size = config.Size;
        XHalf = boxCollider.bounds.size.x / 2;
        YHalf = boxCollider.bounds.size.y / 2;
        ZHalf = boxCollider.bounds.size.z / 2;

        config.FishFlocks.ForEach(config =>
        {
            var fishFlock = runner.InstantiateInRunnerScene(new GameObject("FishFlock")).AddComponent<FishFlock>();
            fishFlock.transform.SetParent(transform);
            fishFlock.Init(config, this);
            FishFlocks.Add(fishFlock);
        });
    }
}
