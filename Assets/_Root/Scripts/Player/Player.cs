using System.Collections.Generic;
using Fusion;
using UnityEngine;

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

    public GyreLine GyreLine { get => _gyreLine; set => _gyreLine = value; }

    public override void Render()
    {
        base.Render();

        HandleInput();
    }

    private void HandleInput()
    {
        if (HasInputAuthority || !_isReady) return;

        _model.UpdatePositionAndRotation(_inputData.PositionHead, _inputData.RotationHead);
        _model.RightHand.transform.SetPositionAndRotation(_inputData.PositionRightHand, _inputData.RotationRightHand);
        _model.LeftHand.transform.SetPositionAndRotation(_inputData.PositionLeftHand, _inputData.RotationLeftHand);

        if (_inputData.MoveBody == 0)
        {
            Tread();
            _model.SetWeightForChainIKHands(1);
        }
        else if (_inputData.MoveBody > 0)
        {
            Swim();
            _model.SetWeightForChainIKHands(0);
        }
        else
        {
            Tread();
            _model.SetWeightForChainIKHands(0);
        }

        _model.SetGrapValueLeftHand(_inputData.GripButtonLeft.IsSet(Buttons.GripButtonLeft));
        _model.SetGrapValueRightHand(_inputData.GripButtonRight.IsSet(Buttons.GripButtonRight));
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
            _playerAudio.RemoveAudio();
        }

        Tread();

        _isReady = true;
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
