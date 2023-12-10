using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public partial class Player : PTBehaviour
{
    enum Buttons
    {
        GrapLeft = 0,
        GrapRight = 1
    }

    public struct InputData : INetworkInput
    {
        public Vector2 Move;
        public NetworkButtons GrapLeftValue;
        public NetworkButtons GrapRightValue;
    }

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _model;
    [SerializeField] private Camera _camera;
    [SerializeField] private List<ChainIKConstraint> _chainIKHands;
    [SerializeField] private Hand _leftHand;
    [SerializeField] private Hand _rightHand;
    private List<Coroutine> _coroutines = new List<Coroutine>();

    public override void Spawned()
    {
        base.Spawned();

        Tread();
        RelocationModel();
    }

    public override void Render()
    {
        base.Render();

        RelocationModel();
        _model.localRotation = Quaternion.identity;
    }

    private void Swim()
    {
        _animator.SetBool("IsSwimming", true);
        SetWeightForChainIKHands(0);
    }

    private void Tread()
    {
        _animator.SetBool("IsSwimming", false);
        SetWeightForChainIKHands(1);
    }

    private void SetWeightForChainIKHands(float weight)
    {
        _coroutines.ForEach(coroutine => { if (coroutine != null) StopCoroutine(coroutine); });
        _coroutines.Clear();

        _chainIKHands.ForEach(chainIKHand =>
        {
            var coroutine = StartCoroutine(SetWeightForChainIKHand(chainIKHand, weight, 0.25f));
            _coroutines.Add(coroutine);
        });
    }

    private IEnumerator SetWeightForChainIKHand(ChainIKConstraint chainIK, float weight, float time)
    {
        if (weight != chainIK.weight)
        {
            var weightCurrent = chainIK.weight;
            while (true)
            {
                float offset = weight - weightCurrent;
                if (offset > Runner.DeltaTime / time)
                {
                    weightCurrent = weightCurrent + offset * Runner.DeltaTime / time;
                    chainIK.weight = weightCurrent;
                    yield return null;
                }
                else
                {
                    chainIK.weight = weight;
                    break;
                }
            }
        }
    }

    private void RelocationModel()
    {
        _model.transform.position += _head.InverseTransformPoint(_camera.transform.position);
    }
}
