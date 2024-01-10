using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.AddressableAssets;

public partial class Player : PTBehaviour
{
    [SerializeField] private GyreLine _gyreLine;
    [SerializeField] private PlayerAudio _playerAudio;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Model _modelPrefab;
    [SerializeField] private Device _devicePrefab;
    [SerializeField] private Transform _body;

    [Networked] private InputData _inputData { get; set; }

    private Device _device;
    private Model _model;
    private bool _isReady;
    private FlashLight _flashLight;

    public GyreLine GyreLine { get => _gyreLine; set => _gyreLine = value; }

    public override void Render()
    {
        base.Render();

        if (HasInputAuthority || !_isReady) return;

        HandleInput(_inputData.MoveBody, _inputData.TriggerButtonRight.IsSet(Buttons.TriggerButtonRight),
                    _inputData.GripButtonLeft.IsSet(Buttons.GripButtonLeft), _inputData.GripButtonRight.IsSet(Buttons.GripButtonRight),
                    _inputData.PositionHead, _inputData.RotationHead,
                    _inputData.PositionRightHand, _inputData.RotationRightHand,
                    _inputData.PositionLeftHand, _inputData.RotationLeftHand);
    }

    private void HandleInput(float moveY, bool triggerValueRightHand,
                            bool grapValueLeftHand, bool grapValueRightHand,
                            Vector3 positionHead, Quaternion rotationHead,
                            Vector3 positionRightHand, Quaternion rotationRightHand,
                            Vector3 positionLeftHand, Quaternion rotationLeftHand)
    {
        _model.UpdatePositionAndRotation(positionHead, rotationHead, positionRightHand, rotationRightHand, positionLeftHand, rotationLeftHand, Runner);

        if (moveY == 0)
        {
            Tread();
            _model.SetWeightForChainIKHands(1);
        }
        else if (moveY > 0)
        {
            Swim();
            _model.SetWeightForChainIKHands(0);
        }
        else
        {
            Tread();
            _model.SetWeightForChainIKHands(0);
        }

        _model.SetGrapValueLeftHand(grapValueLeftHand);
        _model.SetGrapValueRightHand(grapValueRightHand);

        _flashLight.CheckFlashLightFar(triggerValueRightHand);
    }

    public override void Spawned()
    {
        base.Spawned();

        _model = Runner.InstantiateInRunnerScene(_modelPrefab);
        _model.transform.SetParent(_body);
        _model.transform.localPosition = Vector3.zero;
        _model.transform.localRotation = Quaternion.identity;

        if (HasInputAuthority)
        {
            _model.LeftHand.Grapped += () => _device.LeftHandController.SendHapticImpulse(1, 0.5f);
            _model.RightHand.Grapped += () => _device.RightHandController.SendHapticImpulse(1, 0.5f);
            _model.HideHelmet();

            _device = Runner.InstantiateInRunnerScene(_devicePrefab);
            _device.transform.SetParent(_body);
            _device.transform.localPosition = Vector3.zero;
            _device.transform.localRotation = Quaternion.identity;

            _model.SetupIK(_device.Head.transform, _device.LeftHand.transform, _device.RightHand.transform);
        }
        else
        {
            _model.SetupIK(_model.HeadIK.transform, _model.LeftHandIK.transform, _model.RightHandIK.transform);
            _playerAudio.RemoveAudio();
        }

        Tread();

        Addressables.LoadAssetAsync<GameObject>("FlashLight").Completed += handle =>
        {
            _flashLight = Runner.InstantiateInRunnerScene(handle.Result).GetComponent<FlashLight>();

            if (HasInputAuthority) _flashLight.transform.SetParent(_device.Head);
            else _flashLight.transform.SetParent(_model.Head);

            _flashLight.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            _isReady = true;
        };
    }

    private void Swim()
    {
        _model.Swimming(true);
        _playerAudio.ChangeSnapshotToSwimming();
    }

    private void Tread()
    {
        _model.Swimming(false);
        _playerAudio.ChangeSnapshotToBreathing();
    }
}
