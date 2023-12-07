using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

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

        _animator.SetBool("IsSwimming", false);
    }

    private void OnInput(NetworkRunner runner, NetworkInput input)
    {
        input.Set(new InputData()
        {
            Move = _inputAsset.Player.Move.ReadValue<Vector2>(),
            GrapLeft = _inputAsset.Player.GrapLeft.ReadValue<float>(),
            GrapRight = _inputAsset.Player.GrapRight.ReadValue<float>(),
        });
    }
}
