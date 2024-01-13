using System;
using System.Collections;
using DG.Tweening;
using Fusion;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.Universal;

public partial class Player
{
    public static Player player;
    [SerializeField] private InputAsset _inputAsset;
    private int _lastStateRotateBody;
    private float _timerRotateBody;
    private ColorAdjustments _colorAdjustments;
    private Fog _fog;

    public override void SpawnedClient()
    {
        if (HasInputAuthority)
        {
            player = this;

            _inputAsset = new InputAsset();

            var events = Runner.GetComponent<NetworkEvents>();
            events.OnInput = new NetworkEvents.InputEvent();
            events.OnInput.AddListener(OnInput);

            StartCoroutine(CheckAssetLoadDone());
        }
        else
        {
            Tread();
        }
    }

    private IEnumerator CheckAssetLoadDone()
    {
        var eventScene = Runner.GetComponent<EventScene>();

        while (true)
        {
            if (eventScene.RoomType != RoomType.None)
            {
                Addressables.LoadAssetAsync<GameObject>("Audio").Completed += handle =>
                {
                    _playerAudio = Runner.InstantiateInRunnerScene(handle.Result).GetComponent<PlayerAudio>();
                    _playerAudio.transform.SetParent(_device.Camera.transform);
                    _playerAudio.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                    _playerAudio.transform.localScale = Vector3.one;

                    Addressables.LoadAssetAsync<GameObject>("Effect").Completed += handle =>
                    {
                        _playerEffect = Runner.InstantiateInRunnerScene(handle.Result).GetComponent<PlayerEffect>();
                        _playerEffect.transform.SetParent(_device.Camera.transform);
                        _playerEffect.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        _playerEffect.transform.localScale = Vector3.one;

                        Addressables.LoadAssetAsync<GameObject>("Fog" + eventScene.RoomType.ToString()).Completed += handle =>
                        {
                            _fog = Runner.InstantiateInRunnerScene(handle.Result).GetComponent<Fog>();
                            _fog.transform.SetParent(transform);
                            _fog.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                            _fog.transform.localScale = Vector3.one;

                            Addressables.LoadAssetAsync<GameObject>("Dust").Completed += handle =>
                            {
                                var dust = Runner.InstantiateInRunnerScene(handle.Result);
                                dust.transform.SetParent(_model.Head);
                                dust.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                                dust.transform.localScale = Vector3.one;

                                if (RunnerController.Instance.Volume.profile.TryGet(out _colorAdjustments))
                                {
                                    EnableEyes();
                                    Tread();
                                    _isReady = true;
                                }
                            };
                        };
                    };
                };

                break;
            }

            yield return null;
        }
    }

    public void EnableEyes(Action action = null)
    {
        _inputAsset.Enable();
        _colorAdjustments.postExposure.value = -10;
        DOTween.To(() => _colorAdjustments.postExposure.value, (value) => _colorAdjustments.postExposure.value = value, 0, 2);
    }

    public void DisableEyes(Action action = null)
    {
        _inputAsset.Disable();
        _colorAdjustments.postExposure.value = 0;
        DOTween.To(() => _colorAdjustments.postExposure.value, (value) => _colorAdjustments.postExposure.value = value, -10, 2).onComplete += () =>
        {
            action?.Invoke();
        };
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

        inputData.PositionHead = _model.Head.transform.position;
        inputData.RotationHead = _model.Head.transform.rotation;
        inputData.PositionRightHand = _model.RightHand.transform.position;
        inputData.RotationRightHand = _model.RightHand.transform.rotation;
        inputData.PositionLeftHand = _model.LeftHand.transform.position;
        inputData.RotationLeftHand = _model.LeftHand.transform.rotation;

        input.Set(inputData);
    }

    public override void RenderClient()
    {
        if (!_isReady || !HasInputAuthority || !Runner.ProvideInput) return;

        var moveY = _inputAsset.Player.MoveBody.ReadValue<Vector2>().y;
        var grapValueLeftHand = _inputAsset.Player.GripButtonLeft.IsPressed();
        var grapValueRightHand = _inputAsset.Player.GripButtonRight.IsPressed();
        var triggerValueRightHand = _inputAsset.Player.TriggerButtonRight.IsPressed();

        HandleInput(moveY, triggerValueRightHand,
                    grapValueLeftHand, grapValueRightHand,
                    _device.Head.position, _device.Head.rotation,
                    _device.RightHand.position, _device.RightHand.rotation,
                    _device.LeftHand.position, _device.LeftHand.rotation);

        if (_fog != null)
        {
            _fog.CheckFogFar(triggerValueRightHand);
        }
    }
}
