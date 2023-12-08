using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem.XR;

public partial class Player
{
    private InputAsset _inputAsset;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _model;
    [SerializeField] private Camera _camera;
    [SerializeField] private List<ChainIKConstraint> _chainIKHands;

    public override void SpawnedClient()
    {
        _inputAsset = new InputAsset();
        _inputAsset.Enable();

        var events = Runner.GetComponent<NetworkEvents>();
        events.OnInput = new NetworkEvents.InputEvent();
        events.OnInput.AddListener(OnInput);

        Tread();
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

    public override void RenderClient()
    {
        _model.transform.position += _head.InverseTransformPoint(_camera.transform.position);
        if (_inputAsset != null)
        {
            if (_inputAsset.Player.Move.ReadValue<Vector2>().y > 0)
            {
                Swim();
            }
            else
            {
                Tread();
            }
        }
    }

    private void Swim()
    {
        _animator.SetBool("IsSwimming", true);
        SetWeightForChainIKHand(0);
    }

    private void Tread()
    {
        _animator.SetBool("IsSwimming", false);
        SetWeightForChainIKHand(1);
    }

    private void SetWeightForChainIKHand(float weight)
    {
        _chainIKHands.ForEach(chainIKHand => chainIKHand.weight = weight);
    }
}
