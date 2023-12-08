using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public partial class Player
{
    private InputAsset _inputAsset;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _model;
    [SerializeField] private Camera _camera;

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

    public override void Render()
    {
        _model.transform.position += _head.InverseTransformPoint(_camera.transform.position);
    }
}
