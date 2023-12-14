using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClamShell : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Open()
    {
        _animator.SetBool("IsOpen", true);
    }

    public void Close()
    {
        _animator.SetBool("IsOpen", false);
    }
}
