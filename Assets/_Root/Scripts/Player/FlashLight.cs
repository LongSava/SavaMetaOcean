using System.Collections.Generic;
using UnityEngine;
using VLB;

public class FlashLight : MonoBehaviour
{
    public List<Light> Lights;
    public List<VolumetricLightBeamSD> Beams;
    private bool _lastTrigger;
    private bool _enableLight;

    private void Start()
    {
        EnableFlashLightFar(false);
    }

    public void CheckFlashLightFar(bool triggerValueRightHand)
    {
        if (_lastTrigger != triggerValueRightHand)
        {
            _lastTrigger = triggerValueRightHand;
            if (_lastTrigger == true)
            {
                _enableLight = !_enableLight;
                EnableFlashLightFar(_enableLight);
            }
        }
    }

    public void EnableFlashLightFar(bool enable)
    {
        Lights.ForEach(light =>
        {
            light.intensity = enable ? 200 : 50;
            light.range = enable ? 400 : 100;
        });
    }

    public void EnableLightAndBeam(bool enable)
    {
        EnableLight(enable);
        EnableBeam(enable);
    }

    public void EnableBeam(bool enable)
    {
        Beams.ForEach(beam => beam.enabled = enable);
    }

    public void EnableLight(bool enable)
    {
        Lights.ForEach(light => light.enabled = enable);
    }

    public void EnableClipping(bool enable)
    {
        Beams.ForEach(beam => beam.cameraClippingDistance = enable ? 100 : 0);
    }
}
