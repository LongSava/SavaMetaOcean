using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class Model : MonoBehaviour
{
    public Transform Head;
    public RigBuilder RigBuilder;
    public ChainIKConstraint LeftHandIK;
    public ChainIKConstraint RightHandIK;
    public Hand LeftHand;
    public Hand RightHand;
    public Animator Animator;
    public SkinnedMeshRenderer Helmet;
    public bool IsSwimming;
    public List<Coroutine> Coroutines = new List<Coroutine>();

    public void HideHelmet()
    {
        Helmet.enabled = false;
    }

    public void SetGrapValueLeftHand(float grapValue)
    {
        LeftHand.SetGrapValue(grapValue);
    }

    public void SetGrapValueLeftHand(bool grapValue)
    {
        LeftHand.SetGrapValue(grapValue);
    }

    public void SetGrapValueRightHand(float grapValue)
    {
        RightHand.SetGrapValue(grapValue);
    }

    public void SetGrapValueRightHand(bool grapValue)
    {
        RightHand.SetGrapValue(grapValue);
    }

    public void UpdatePositionAndRotation(Vector3 positionHead, Quaternion rotationHead)
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, rotationHead.eulerAngles.y, transform.rotation.eulerAngles.z));
        Head.rotation = Quaternion.Euler(new Vector3(rotationHead.eulerAngles.x, Head.rotation.eulerAngles.y, Head.rotation.eulerAngles.z));
        transform.position += positionHead - Head.position;
    }

    public void SetupIK(Transform leftHand, Transform rightHand)
    {
        LeftHandIK.data.target = leftHand;
        RightHandIK.data.target = rightHand;
        RigBuilder.Build();
    }

    public void Swimming(bool isSwimming)
    {
        Animator.SetBool("IsSwimming", isSwimming);
        IsSwimming = isSwimming;
    }

    public void SetWeightForChainIKHands(float weight)
    {
        Coroutines.ForEach(coroutine => { if (coroutine != null) StopCoroutine(coroutine); });
        Coroutines.Clear();

        Coroutines.Add(StartCoroutine(SetWeightForChainIKHand(LeftHandIK, weight, 0.3f)));
        Coroutines.Add(StartCoroutine(SetWeightForChainIKHand(RightHandIK, weight, 0.3f)));
    }

    private IEnumerator SetWeightForChainIKHand(ChainIKConstraint chainIK, float weight, float time)
    {
        if (weight != chainIK.weight)
        {
            var weightCurrent = chainIK.weight;
            while (true)
            {
                float offset = weight - weightCurrent;
                if (Mathf.Abs(offset) > Time.fixedDeltaTime / time)
                {
                    weightCurrent += offset * Time.fixedDeltaTime / time;
                    chainIK.weight = weightCurrent;
                    yield return null;
                }
                else
                {
                    chainIK.weight = weight;
                    break;
                }
            }
        }
    }
}
