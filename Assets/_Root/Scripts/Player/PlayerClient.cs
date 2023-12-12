using Fusion;
using UnityEngine;

public partial class Player
{
    [SerializeField] private InputAsset _inputAsset;

    public override void SpawnedClient()
    {
        if (HasInputAuthority)
        {
            _inputAsset = new InputAsset();
            _inputAsset.Enable();

            var events = Runner.GetComponent<NetworkEvents>();
            events.OnInput = new NetworkEvents.InputEvent();
            events.OnInput.AddListener(OnInput);
        }
    }

    private void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var inputData = new InputData();

        inputData.GrapLeftValue.Set(Buttons.GrapLeft, _inputAsset.Player.GrapLeftValue.IsPressed());
        inputData.GrapRightValue.Set(Buttons.GrapRight, _inputAsset.Player.GrapRightValue.IsPressed());

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

        if (_inputAsset.Player.RotateBody.ReadValue<Vector2>().x > 0)
        {
            inputData.RotateBody = 1;
        }
        else if (_inputAsset.Player.RotateBody.ReadValue<Vector2>().x < 0)
        {
            inputData.RotateBody = -1;
        }
        else
        {
            inputData.RotateBody = 0;
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
        if (_inputAsset != null)
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

            _leftHand.SetGrapValue(_inputAsset.Player.GrapLeft.ReadValue<float>());
            _rightHand.SetGrapValue(_inputAsset.Player.GrapRight.ReadValue<float>());

            var mouseMove = _inputAsset.Player.MoveMouse.ReadValue<Vector2>();
            _leftHandDevice.position = _camera.ScreenToWorldPoint(new Vector3(mouseMove.x, mouseMove.y, 1.5f));

            var rotateHead = _inputAsset.Player.RotateHead.ReadValue<float>();
            if (rotateHead != 0)
            {
                _trackedPoseDriver.enabled = false;
                _headDevice.Rotate(Vector3.right * rotateHead * Runner.DeltaTime * 10);
            }
        }
    }
}
