using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering.Universal;

public partial class Player : PTBehaviour
{
    enum Buttons
    {
        GrapLeft = 0,
        GrapRight = 1
    }

    public struct InputData : INetworkInput
    {
        public int MoveBody;
        public int RotateBody;
        public NetworkButtons GrapLeftValue;
        public NetworkButtons GrapRightValue;
        public Vector3 PositionHead;
        public Quaternion RotationHead;
        public Vector3 PositionRightHand;
        public Quaternion RotationRightHand;
        public Vector3 PositionLeftHand;
        public Quaternion RotationLeftHand;
    }

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _bodyOffset;
    [SerializeField] private Camera _camera;
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private UniversalAdditionalCameraData _universalAdditionalCameraData;
    [SerializeField] private List<ChainIKConstraint> _chainIKHands;
    [SerializeField] private Hand _leftHand;
    [SerializeField] private Hand _rightHand;
    [SerializeField] private Transform _headDevice;
    [SerializeField] private Transform _rightHandDevice;
    [SerializeField] private Transform _leftHandDevice;
    private List<Coroutine> _coroutines = new List<Coroutine>();

    public Transform HeadDevice { get => _headDevice; set => _headDevice = value; }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (Runner.IsServer || Object.IsProxy)
        {
            if (GetInput(out InputData input))
            {
                _headDevice.position = input.PositionHead;
                _headDevice.rotation = input.RotationHead;
                _rightHandDevice.position = input.PositionRightHand;
                _rightHandDevice.rotation = input.RotationRightHand;
                _leftHandDevice.position = input.PositionLeftHand;
                _leftHandDevice.rotation = input.RotationLeftHand;
            }
        }
    }

    public override void Spawned()
    {
        base.Spawned();
        Tread();
        if (!HasInputAuthority)
        {
            Destroy(_universalAdditionalCameraData);
            Destroy(_camera);
            Destroy(_audioListener);
        }
    }

    public override void Render()
    {
        base.Render();

        _bodyOffset.position += _headDevice.transform.position - _head.position;
    }

    private void Swim()
    {
        _animator.SetBool("IsSwimming", true);
    }

    private void Tread()
    {
        _animator.SetBool("IsSwimming", false);
    }

    private void SetWeightForChainIKHands(float weight)
    {
        _coroutines.ForEach(coroutine => { if (coroutine != null) StopCoroutine(coroutine); });
        _coroutines.Clear();

        _chainIKHands.ForEach(chainIKHand =>
        {
            var coroutine = StartCoroutine(SetWeightForChainIKHand(chainIKHand, weight, 0.5f));
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
                if (offset > Runner.DeltaTime / time)
                {
                    weightCurrent = weightCurrent + offset * Runner.DeltaTime / time;
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
