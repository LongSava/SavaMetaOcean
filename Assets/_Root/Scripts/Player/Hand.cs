using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : NetworkBehaviour
{
    [SerializeField] private Transform _fishTransform;
    [SerializeField] private List<Finger> _fingers;
    [SerializeField] private XRBaseController _xRBaseController;
    [SerializeField] private AudioSource _fishStruggling;
    [SerializeField] private BoxCollider _boxCollider;
    private Fish _fish;
    private float _grapValue;
    private bool _isImpulseGrap;
    private Dictionary<NetworkBehaviourId, Fish> _fishes = new Dictionary<NetworkBehaviourId, Fish>();

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
        if (_grapValue == 1) Grap();
        else Ungrap();
    }

    private void Grap()
    {
        if (_fish == null && _fishes.Count > 0)
        {
            _fish = _fishes.First().Value;
            _boxCollider.isTrigger = true;
            _fish.Catched(_fishTransform);
            if (!_isImpulseGrap)
            {
                _isImpulseGrap = true;
                _xRBaseController.SendHapticImpulse(1, 0.5f);
                _fishStruggling.Play();
            }
        }
    }

    private void Ungrap()
    {
        if (_fish != null)
        {
            if (_isImpulseGrap)
            {
                _isImpulseGrap = false;
                _xRBaseController.SendHapticImpulse(1, 0.5f);
                _fishStruggling.Stop();
            }
            _fish.Released();
            _boxCollider.isTrigger = false;
            _fish = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fish") && other.TryGetComponent(out Fish fish) && !_fishes.ContainsKey(fish.Id))
        {
            _fishes.Add(fish.Id, fish);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fish") && other.TryGetComponent(out Fish fish) && _fishes.ContainsKey(fish.Id))
        {
            _fishes.Remove(fish.Id);
        }
    }
}
