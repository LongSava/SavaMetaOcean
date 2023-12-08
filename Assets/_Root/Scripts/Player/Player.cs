using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public partial class Player : PTBehaviour
{
    public struct InputData : INetworkInput
    {
        public Vector2 Move;
        public float GrapLeft;
        public float GrapRight;
    }

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _model;
    [SerializeField] private Camera _camera;
    [SerializeField] private List<ChainIKConstraint> _chainIKHands;
    [SerializeField] private Hand LeftHand;
    [SerializeField] private Hand RightHand;

    public override void Spawned()
    {
        base.Spawned();

        Tread();
    }

    public override void Render()
    {
        base.Render();

        _model.transform.position += _head.InverseTransformPoint(_camera.transform.position);
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
