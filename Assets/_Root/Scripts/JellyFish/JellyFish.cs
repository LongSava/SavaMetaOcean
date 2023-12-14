using UnityEngine;

public class JellyFish : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] private float _speedAnimator;
    [SerializeField] private float _speedMove;

    private void Start()
    {
        _animator.speed = _speedAnimator;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * _speedMove * Time.deltaTime);
    }
}
