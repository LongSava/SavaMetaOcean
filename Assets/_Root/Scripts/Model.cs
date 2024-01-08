using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class Model : MonoBehaviour
{
    public Transform Target;
    public Transform Head;
    public RigBuilder RigBuilder;
    public RotationConstraint ModelConstraint;
    public RotationConstraint HeadConstraint;
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

    public void SetupTarget(Transform target)
    {
        Target = target;
    }

    private void Update()
    {
        transform.position += Target.position - Head.position;
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, IsSwimming ? Head.rotation : Quaternion.identity, Time.deltaTime * 30);
    }

    public void SetupConstraint(Transform head, Transform leftHand, Transform rightHand)
    {
        var source = new ConstraintSource
        {
            sourceTransform = head
        };

        ModelConstraint.SetSource(0, source);
        ModelConstraint.constraintActive = true;

        HeadConstraint.SetSource(0, source);
        HeadConstraint.constraintActive = true;

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
        Coroutines.Add(StartCoroutine(SetWeightForChainIKHand(LeftHandIK, weight, 0.3f)));
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
