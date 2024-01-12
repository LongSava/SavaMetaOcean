using AtmosphericHeightFog;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public HeightFogGlobal HeightFogGlobal;
    private bool _lastTrigger;
    private bool _enableFogFar;

    private void Start()
    {
        EnableFogFar(true);
    }

    public void CheckFogFar(bool triggerValueRightHand)
    {

        if (_lastTrigger != triggerValueRightHand)
        {
            _lastTrigger = triggerValueRightHand;
            if (_lastTrigger == true)
            {
                _enableFogFar = !_enableFogFar;
                EnableFogFar(_enableFogFar);
            }
        }
    }

    public void EnableFogFar(bool enable)
    {
        _enableFogFar = enable;
        HeightFogGlobal.fogHeightStart = enable ? -Config.Data.Vision.Range.Far : -Config.Data.Vision.Range.Near;
        HeightFogGlobal.fogHeightEnd = enable ? Config.Data.Vision.Range.Far : Config.Data.Vision.Range.Near;
        HeightFogGlobal.fogDistanceStart = enable ? Config.Data.Vision.Range.Far / 2 : Config.Data.Vision.Range.Near / 2;
        HeightFogGlobal.fogDistanceEnd = enable ? Config.Data.Vision.Range.Far : Config.Data.Vision.Range.Near;
    }
}
