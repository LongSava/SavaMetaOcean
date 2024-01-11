using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Finger : MonoBehaviour
{
    [SerializeField] private ChainIKConstraint _fingerTip;
    [SerializeField] private Vector3 _fingerTipOffset;

    private void Start()
    {
        Grap(0);
        _fingerTip.transform.position = transform.position + _fingerTipOffset.x * transform.right + _fingerTipOffset.y * transform.up + _fingerTipOffset.z * transform.forward;
    }

    public void Grap(float grapValue)
    {
        _fingerTip.weight = grapValue;
    }
}
