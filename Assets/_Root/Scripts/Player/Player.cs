using Fusion;
using UnityEngine;

public partial class Player : PTBehaviour
{
    public struct InputData : INetworkInput
    {
        public Vector2 Move;
        public float GrapLeft;
        public float GrapRight;
    }

    [SerializeField] private Animator _animator;
}
