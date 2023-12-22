using System.Collections;
using DG.Tweening;
using Fusion;
using UnityEngine;
using UnityEngine.AddressableAssets;

public partial class Player
{
    [SerializeField] private InputAsset _inputAsset;
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
            _inputAsset.Enable();

            var events = Runner.GetComponent<NetworkEvents>();
            events.OnInput = new NetworkEvents.InputEvent();
            events.OnInput.AddListener(OnInput);

            Addressables.LoadAssetAsync<GameObject>("Ocean").Completed += handle => Runner.InstantiateInRunnerScene(handle.Result);
            Addressables.LoadAssetAsync<GameObject>("Gyre").Completed += handle => Runner.InstantiateInRunnerScene(handle.Result);
            Addressables.LoadAssetAsync<GameObject>("ClamShells").Completed += handle => Runner.InstantiateInRunnerScene(handle.Result);
            Addressables.LoadAssetAsync<GameObject>("JellyFishes").Completed += handle => Runner.InstantiateInRunnerScene(handle.Result);
            Addressables.LoadAssetAsync<GameObject>("BubblesCommon").Completed += handle => Runner.InstantiateInRunnerScene(handle.Result);
            Addressables.LoadAssetAsync<GameObject>("SunLight").Completed += handle => Runner.InstantiateInRunnerScene(handle.Result);
            Addressables.LoadAssetAsync<GameObject>("Dust").Completed += handle =>
            {
                var dust = Runner.InstantiateInRunnerScene(handle.Result);
                dust.transform.SetParent(transform);
                dust.transform.localPosition = Vector3.zero;
                dust.transform.localScale = Vector3.one;
            };

            _eyes.material.DOFade(0, 2);
        }
    }

    private void OnInput(NetworkRunner runner, NetworkInput input)
    {
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

        inputData.PositionHead = _headDevice.position;
        inputData.RotationHead = _headDevice.rotation;
        inputData.PositionRightHand = _rightHandDevice.position;
        inputData.RotationRightHand = _rightHandDevice.rotation;
        inputData.PositionLeftHand = _leftHandDevice.position;
        inputData.RotationLeftHand = _leftHandDevice.rotation;

        input.Set(inputData);
    }

    public override void RenderClient()
    {
        if (HasInputAuthority && Runner.ProvideInput)
        {
            var moveY = _inputAsset.Player.MoveBody.ReadValue<Vector2>().y;
            if (moveY == 0)
            {
                Tread();
                SetWeightForChainIKHands(1);
            }
            else if (moveY > 0)
            {
                Swim();
                SetWeightForChainIKHands(0);
            }
            else
            {
                Tread();
                SetWeightForChainIKHands(0);
            }

            _leftHand.SetGrapValue(_inputAsset.Player.GripLeft.ReadValue<float>());
            _rightHand.SetGrapValue(_inputAsset.Player.GripRight.ReadValue<float>());

            var mouseMove = _inputAsset.Player.MoveMouse.ReadValue<Vector2>();
            if (mouseMove != Vector2.zero)
            {
                _leftHandDevice.position = _camera.ScreenToWorldPoint(new Vector3(mouseMove.x, mouseMove.y, 1.5f));
            }

            var rotateHead = _inputAsset.Player.RotateHead.ReadValue<float>();
            if (rotateHead != 0)
            {
                _trackedPoseDriver.enabled = false;
                _headDevice.Rotate(30 * rotateHead * Runner.DeltaTime * Vector3.right);
            }
        }
    }
}
