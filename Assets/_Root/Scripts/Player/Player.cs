using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

public partial class Player : PTBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _bodyOffset;
    [SerializeField] private UniversalAdditionalCameraData _universalAdditionalCameraData;
    [SerializeField] private Camera _camera;
    [SerializeField] private TrackedPoseDriver _trackedPoseDriver;
    [SerializeField] private XRBaseController _xRLeftBaseController;
    [SerializeField] private XRBaseController _xRRightBaseController;
    [SerializeField] private List<ChainIKConstraint> _chainIKHands;
    [SerializeField] private Hand _leftHand;
    [SerializeField] private Hand _rightHand;
    [SerializeField] private Transform _headDevice;
    [SerializeField] private Transform _rightHandDevice;
    [SerializeField] private Transform _leftHandDevice;
    [SerializeField] private Vector3 _offsetHead;
    [Networked] private InputData _inputData { get; set; }
    private List<Coroutine> _coroutines = new List<Coroutine>();
    private bool isSwimming;

    public Transform HeadDevice { get => _headDevice; set => _headDevice = value; }

    private void HandleInput(InputData input)
    {
        _headDevice.position = input.PositionHead;
        _headDevice.rotation = input.RotationHead;
        _rightHandDevice.position = input.PositionRightHand;
        _rightHandDevice.rotation = input.RotationRightHand;
        _leftHandDevice.position = input.PositionLeftHand;
        _leftHandDevice.rotation = input.RotationLeftHand;

        if (input.MoveBody == 0)
        {
            Tread();
            SetWeightForChainIKHands(1);
        }
        else if (input.MoveBody > 0)
        {
            Swim();
            SetWeightForChainIKHands(0);
        }
        else
        {
            Tread();
            SetWeightForChainIKHands(0);
        }

        _leftHand.SetGrapValue(input.GripButtonLeft.IsSet(Buttons.GripButtonLeft));
        _rightHand.SetGrapValue(input.GripButtonRight.IsSet(Buttons.GripButtonRight));
    }

    public override void Spawned()
    {
        base.Spawned();
        Tread();
        if (!HasInputAuthority)
        {
            Destroy(_universalAdditionalCameraData);
            Destroy(_camera);
            Destroy(_trackedPoseDriver);
            Destroy(_xRLeftBaseController);
            Destroy(_xRRightBaseController);
        }
    }

    public override void Render()
    {
        base.Render();

        _bodyOffset.position += _headDevice.position - _head.position + _headDevice.forward * _offsetHead.z + _headDevice.right * _offsetHead.x + _headDevice.up * _offsetHead.y;
        if (isSwimming)
        {
            _bodyOffset.rotation = Quaternion.RotateTowards(_bodyOffset.rotation, _headDevice.rotation, Runner.DeltaTime * 30);
        }
        else
        {
            _bodyOffset.rotation = Quaternion.RotateTowards(_bodyOffset.rotation, Quaternion.identity, Runner.DeltaTime * 30);
        }
    }

    private void Swim()
    {
        _animator.SetBool("IsSwimming", true);
        isSwimming = true;
    }

    private void Tread()
    {
        _animator.SetBool("IsSwimming", false);
        isSwimming = false;
    }

    private void SetWeightForChainIKHands(float weight)
    {
        _coroutines.ForEach(coroutine => { if (coroutine != null) StopCoroutine(coroutine); });
        _coroutines.Clear();

        _chainIKHands.ForEach(chainIKHand =>
        {
            var coroutine = StartCoroutine(SetWeightForChainIKHand(chainIKHand, weight, 0.3f));
            _coroutines.Add(coroutine);
        });
    }

    private IEnumerator SetWeightForChainIKHand(ChainIKConstraint chainIK, float weight, float time)
    {
        if (weight != chainIK.weight)
        {
            var weightCurrent = chainIK.weight;
            while (true)
            {
                float offset = weight - weightCurrent;
                if (Mathf.Abs(offset) > Runner.DeltaTime / time)
                {
                    weightCurrent += offset * Runner.DeltaTime / time;
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
