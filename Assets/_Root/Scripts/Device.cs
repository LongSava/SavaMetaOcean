using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Device : MonoBehaviour
{
    public Camera Head;
    public Transform LeftHand;
    public Transform RightHand;
    public XRBaseController LeftHandController;
    public XRBaseController RightHandController;

    private void Update()
    {
        transform.localPosition = -Head.transform.localPosition;
    }
}
