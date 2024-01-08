using System.Collections;
using UnityEngine;

public class ClamShell : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _bubblesSound;
    private bool _isParticlePlaying;
    private Coroutine _coroutine;

    public void Open()
    {
        _animator.SetBool("IsOpen", true);

        if (!_isParticlePlaying)
        {
            _coroutine = StartCoroutine(PlayParticle());
            _isParticlePlaying = true;
        }
    }

    public IEnumerator PlayParticle()
    {
        yield return new WaitForSeconds(1);

        _particleSystem.Play();
        _bubblesSound.Play();
    }

    public void Close()
    {
        _animator.SetBool("IsOpen", false);
        _isParticlePlaying = false;
        StopCoroutine(_coroutine);
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
