using System.Collections.Generic;
using UnityEngine;

public class FishArea : MonoBehaviour
{
    [SerializeField] private int _maxFish;
    [SerializeField] private List<FishFlock> _fishFlocks;

    public List<FishFlock> FishFlocks { get => _fishFlocks; set => _fishFlocks = value; }
    public int MaxFish { get => _maxFish; set => _maxFish = value; }
}
