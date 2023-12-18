using Fusion;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Finger : NetworkBehaviour
{
    [SerializeField] private ChainIKConstraint _fingerTip;
    [SerializeField] private Vector3 _fingerTipOffset;

    public override void Spawned()
    {
        Grap(0);
        _fingerTip.transform.position = transform.position + _fingerTipOffset.x * transform.right + _fingerTipOffset.y * transform.up + _fingerTipOffset.z * transform.forward;
    }

    public void Grap(float grapValue)
    {
        _fingerTip.weight = grapValue;
    }
}
