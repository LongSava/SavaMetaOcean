using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public List<Light> Lights;
    private bool _lastTrigger;
    private bool _enableLight;

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
            light.intensity = enable ? 150 : 75;
            light.range = enable ? 200 : 100;
        });
    }
}
