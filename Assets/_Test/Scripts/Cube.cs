using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Cube : NetworkBehaviour
{
    struct InputData : INetworkInput
    {
        public Vector2 Move;
        public Vector3 Position;
    }

    public InputAsset InputAsset;
    public float SpeedMove;
    public float SpeedRotate;
    public Vector3 Position;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _model;
    [SerializeField] private Camera _camera;

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            InputAsset = new InputAsset();
            InputAsset.Enable();

            var events = Runner.GetComponent<NetworkEvents>();
            events.OnInput = new NetworkEvents.InputEvent();
            events.OnInput.AddListener(OnInput);
        }
    }

    private void OnInput(NetworkRunner runner, NetworkInput input)
    {
        input.Set(new InputData()
        {
            Move = InputAsset.Player.Move.ReadValue<Vector2>(),
            Position = Position
        });
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.IsServer && GetInput(out InputData input))
        {
            var position = new Vector3(0, 0, input.Move.y * SpeedMove) * Runner.DeltaTime;
            transform.Translate(position);

            var rotation = new Vector3(0, input.Move.x * SpeedRotate, 0) * Runner.DeltaTime;
            transform.Rotate(rotation);

            // transform.position = input.Position;
        }
    }

    public override void Render()
    {
        if (HasInputAuthority && Input.GetMouseButton(0))
        {
            Position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            transform.position = Position;
        }
        // _model.transform.position += _head.InverseTransformPoint(_camera.transform.position);
        _model.localRotation = Quaternion.identity;
    }
}
