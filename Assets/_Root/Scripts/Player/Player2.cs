using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Unity.XR.CoreUtils;
using UnityEngine;

public struct InputData1 : INetworkInput
{
    public int DirectionMove;
    public int DirectionRotate;
}

public class Player2 : NetworkBehaviour
{
    public float SpeedMove = 5;
    public float SpeedRotate = 45;
    public Rigidbody Rigidbody;
    public Transform Body;
    public Device XRPrefab;
    public Model ModelPrefab;

    private Device xr;
    private Model model;

    public override void Spawned()
    {
        var events = Runner.GetComponent<NetworkEvents>();
        events.OnInput = new NetworkEvents.InputEvent();
        events.OnInput.AddListener(OnInput);

        model = Runner.InstantiateInRunnerScene(ModelPrefab);
        model.transform.SetParent(Body);

        if (HasInputAuthority)
        {
            xr = Runner.InstantiateInRunnerScene(XRPrefab);
            xr.transform.SetParent(Body);

            model.SetupIK(xr.Head.transform, xr.LeftHand.transform, xr.RightHand.transform);
        }
    }

    private void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var inputData = new InputData1
        {
            DirectionMove = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0,
            DirectionRotate = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0,
        };
        input.Set(inputData);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out InputData1 inputData))
        {
            if (Runner.IsServer)
            {
                transform.Rotate(inputData.DirectionRotate * Runner.DeltaTime * SpeedRotate * Vector3.up);
                Rigidbody.MovePosition(transform.position + inputData.DirectionMove * Runner.DeltaTime * SpeedMove * transform.forward);
            }

            model.Swimming(inputData.DirectionMove > 0);
        }
    }
}
