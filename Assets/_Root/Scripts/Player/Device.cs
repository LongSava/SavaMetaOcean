using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.XR.Interaction.Toolkit;

public class Device : MonoBehaviour
{
    public Camera Camera;
    public XRBaseController LeftHandController;
    public XRBaseController RightHandController;
    public Transform Head;
    public Transform LeftHand;
    public Transform RightHand;
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
                Lights.ForEach(light =>
                {
                    light.intensity = _enableLight ? 200 : 50;
                    light.range = _enableLight ? 400 : 100;
                });
            }
        }
    }
}
