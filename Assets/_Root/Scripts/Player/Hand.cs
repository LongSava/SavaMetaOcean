using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Hand : NetworkBehaviour
{
    [SerializeField] private Transform _fishTransform;
    [SerializeField] private List<Finger> _fingers;
    [SerializeField] private Fish _fish;
    [SerializeField] private float _grapValue;

    public void SetFish(Fish fish)
    {
        _fish = fish;
    }

    public void SetGrapValue(bool isGrapped)
    {
        if (isGrapped) _grapValue = 1;
        else _grapValue = 0;
        _fingers.ForEach(finger => finger.Grap(_grapValue));
    }

    public void SetGrapValue(float grapValue)
    {
        _grapValue = grapValue;
        _fingers.ForEach(finger => finger.Grap(grapValue));
    }

    public override void Render()
    {
        if (_fish != null)
        {
            if (_grapValue == 1)
            {
                _fish.Catched(_fishTransform);
            }
            else
            {
                _fish.Released();
            }
        }
    }
}
