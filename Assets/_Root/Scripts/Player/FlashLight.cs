using System.Collections.Generic;
using UnityEngine;
using VLB;

public class FlashLight : MonoBehaviour
{
    public List<Light> Lights;
    public List<VolumetricLightBeamSD> Beams;
    private bool _lastTrigger;
    public bool EnableLightFar;

    private void Start()
    {
        EnableFlashLightFar(true);
    }

    public void CheckFlashLightFar(bool triggerValueRightHand)
    {
        if (_lastTrigger != triggerValueRightHand)
        {
            _lastTrigger = triggerValueRightHand;
            if (_lastTrigger == true)
            {
                EnableLightFar = !EnableLightFar;
                EnableFlashLightFar(EnableLightFar);
            }
        }
    }

    public void EnableFlashLightFar(bool enable)
    {
        EnableLightFar = enable;
        Lights.ForEach(light =>
        {
            light.intensity = enable ? Config.Data.Vision.Intensity.Far / 2 : Config.Data.Vision.Intensity.Near / 2;
            light.range = enable ? Config.Data.Vision.Range.Far : Config.Data.Vision.Range.Near;
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

    public void EnableImportantRenderMode(bool enable)
    {
        Lights.ForEach(light => light.renderMode = enable ? LightRenderMode.ForcePixel : LightRenderMode.Auto);
    }
}
