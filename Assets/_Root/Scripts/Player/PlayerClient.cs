using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public partial class Player
{
    private InputAsset _inputAsset;

    public override void SpawnedClient()
    {
        _inputAsset = new InputAsset();
        _inputAsset.Enable();

        var events = Runner.GetComponent<NetworkEvents>();
        events.OnInput = new NetworkEvents.InputEvent();
        events.OnInput.AddListener(OnInput);
    }

    private void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var inputData = new InputData();

        inputData.Move = _inputAsset.Player.Move.ReadValue<Vector2>();
        inputData.GrapLeftValue.Set(Buttons.GrapLeft, _inputAsset.Player.GrapLeftValue.IsPressed());
        inputData.GrapRightValue.Set(Buttons.GrapRight, _inputAsset.Player.GrapRightValue.IsPressed());

        input.Set(inputData);
    }

    public override void RenderClient()
    {
        if (_inputAsset != null)
        {
            if (_inputAsset.Player.Move.ReadValue<Vector2>().y > 0) Swim();
            else Tread();

            _leftHand.SetGrapValue(_inputAsset.Player.GrapLeft.ReadValue<float>());
            _rightHand.SetGrapValue(_inputAsset.Player.GrapRight.ReadValue<float>());
        }
    }
}
