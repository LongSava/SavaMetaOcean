using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : NetworkBehaviour
{
    [SerializeField] private Transform _fishTransform;
    [SerializeField] private List<Finger> _fingers;
    [SerializeField] private XRBaseController _xRBaseController;
    [SerializeField] private AudioSource _fishStruggling;
    private Fish _fish;
    private float _grapValue;
    private float _grapValueOld;
    private bool _isSendingHaptic;

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
                if (!_isSendingHaptic)
                {
                    _isSendingHaptic = true;
                    _xRBaseController.SendHapticImpulse(1, 0.5f);
                    _fishStruggling.Play();
                }
            }
            else
            {
                _fish.Released();
                if (_grapValueOld == 1)
                {
                    _fish = null;
                }
                if (_isSendingHaptic)
                {
                    _isSendingHaptic = false;
                    _xRBaseController.SendHapticImpulse(1, 0.5f);
                    _fishStruggling.Stop();
                }
            }
        }
        _grapValueOld = _grapValue;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_fish == null && other.CompareTag("Fish"))
        {
            var fish = other.GetComponent<Fish>();
            if (fish.IsRelease)
            {
                _fish = fish;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fish") && _grapValue == 0)
        {
            _fish = null;
        }
    }

    private IEnumerator SendHapticImpulse(float amplitude, float duration)
    {
        if (!_isSendingHaptic)
        {
            _isSendingHaptic = true;
            _xRBaseController.SendHapticImpulse(amplitude, duration);
            yield return new WaitForSeconds(duration * 2);
            _isSendingHaptic = false;
        }
    }
}
