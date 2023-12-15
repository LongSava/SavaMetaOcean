using System.Collections;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Close();
        }
    }
}
