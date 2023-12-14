using System.Collections.Generic;
using UnityEngine;

public class FishArea : MonoBehaviour
{
    public List<FishFlock> FishFlocks = new List<FishFlock>();
    public float XHalf;
    public float YHalf;
    public float ZHalf;
    public int MaxFish;
    public int NumberFlock;

    private void Awake()
    {
        var boxCollider = GetComponent<BoxCollider>();
        XHalf = boxCollider.bounds.size.x / 2;
        YHalf = boxCollider.bounds.size.y / 2;
        ZHalf = boxCollider.bounds.size.z / 2;

        for (int i = 0; i < NumberFlock; i++)
        {
            var flock = Instantiate(Config.Data.Fish.Flock.Object, transform);
            flock.SetFishArea(this);
            FishFlocks.Add(flock);
        }
    }
}
