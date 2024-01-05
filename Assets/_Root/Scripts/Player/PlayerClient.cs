using System;
using DG.Tweening;
using Fusion;
using UnityEngine;
using UnityEngine.AddressableAssets;

public partial class Player
{
    [SerializeField] private InputAsset _inputAsset;
    [SerializeField] private ParticleSystem _particleSystem;
    private int _lastStateRotateBody;
    private float _timerRotateBody;

    public override void FixedUpdateClient()
    {
        if (!HasInputAuthority)
        {
            HandleInput(_inputData);
        }
    }

    public override void SpawnedClient()
    {
        if (HasInputAuthority)
        {
            _inputAsset = new InputAsset();

            var events = Runner.GetComponent<NetworkEvents>();
            events.OnInput = new NetworkEvents.InputEvent();
            events.OnInput.AddListener(OnInput);

            Runner.GetComponent<EventScene>().OnAssetLoadDone += (roomType) =>
            {
                Addressables.LoadAssetAsync<GameObject>("Fog" + roomType.ToString()).Completed += handle =>
                {
                    var fogOcean = Runner.InstantiateInRunnerScene(handle.Result);
                    fogOcean.transform.SetParent(transform);
                    EnableEyes();
                };
            };
        }
    }

    public void DisableEyes(Action action = null)
    {
        if (HasInputAuthority)
        {
            _inputAsset.Disable();
            _eyes.enabled = true;
            _eyes.material.DOFade(1, 2).OnComplete(() =>
            {
                action?.Invoke();
            });
        }
    }

    public void EnableEyes(Action action = null)
    {
        if (HasInputAuthority)
        {
            _eyes.material.DOFade(0, 2).OnComplete(() =>
            {
                _inputAsset.Enable();
                _eyes.enabled = false;
                action?.Invoke();
            });
        }
    }

    private void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (!_isReady) return;

        var inputData = new InputData();

        inputData.GripButtonLeft.Set(Buttons.GripButtonLeft, _inputAsset.Player.GripButtonLeft.IsPressed());
        inputData.GripButtonRight.Set(Buttons.GripButtonRight, _inputAsset.Player.GripButtonRight.IsPressed());
        inputData.TriggerButtonRight.Set(Buttons.TriggerButtonRight, _inputAsset.Player.TriggerButtonRight.IsPressed());

        if (_inputAsset.Player.MoveBody.ReadValue<Vector2>().y > 0)
        {
            inputData.MoveBody = 1;
        }
        else if (_inputAsset.Player.MoveBody.ReadValue<Vector2>().y < 0)
        {
            inputData.MoveBody = -1;
        }
        else
        {
            inputData.MoveBody = 0;
        }

        if (_timerRotateBody < 0)
        {
            var rotateBody = _inputAsset.Player.RotateBody.ReadValue<Vector2>();

            if (rotateBody.x > 0.9f && _lastStateRotateBody == 0)
            {
                _lastStateRotateBody = 1;
                inputData.RotateBody = 1;
                _timerRotateBody = 0.1f;
            }
            else if (rotateBody.x < -0.9f && _lastStateRotateBody == 0)
            {
                _lastStateRotateBody = -1;
                inputData.RotateBody = -1;
                _timerRotateBody = 0.1f;
            }
            else if (rotateBody.x == 0)
            {
                _lastStateRotateBody = 0;
                inputData.RotateBody = 0;
                _timerRotateBody = 0;
            }
        }
        else
        {
            _timerRotateBody -= Runner.DeltaTime;
            inputData.RotateBody = _lastStateRotateBody;
        }

        inputData.PositionHead = _device.Head.transform.position;
        inputData.RotationHead = _device.Head.transform.rotation;
        inputData.PositionRightHand = _device.RightHand.transform.position;
        inputData.RotationRightHand = _device.RightHand.transform.rotation;
        inputData.PositionLeftHand = _device.LeftHand.transform.position;
        inputData.RotationLeftHand = _device.LeftHand.transform.rotation;

        input.Set(inputData);
    }

    public override void RenderClient()
    {
        if (!_isReady) return;

        if (HasInputAuthority && Runner.ProvideInput)
        {
            var moveY = _inputAsset.Player.MoveBody.ReadValue<Vector2>().y;
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

            _model.SetGrapValueLeftHand(_inputAsset.Player.GripLeft.ReadValue<float>());
            _model.SetGrapValueRightHand(_inputAsset.Player.GripRight.ReadValue<float>());

            var mouseMove = _inputAsset.Player.MoveMouse.ReadValue<Vector2>();
            if (mouseMove != Vector2.zero)
            {
                _device.LeftHand.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mouseMove.x, mouseMove.y, 1.5f));
            }

            var rotateHead = _inputAsset.Player.RotateHead.ReadValue<float>();
            if (rotateHead != 0)
            {
                _device.Head.transform.Rotate(30 * rotateHead * Runner.DeltaTime * Vector3.right);
            }
        }
    }
}
