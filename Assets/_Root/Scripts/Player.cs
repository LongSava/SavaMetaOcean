using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator.SetBool("IsSwimming", false);
    }
}
