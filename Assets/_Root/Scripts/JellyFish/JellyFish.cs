using UnityEngine;

public class JellyFish : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] private float _speedAnimator;
    [SerializeField] private float _speedMove;
    private float _positionYOrigin;
    private Vector3 _direction = Vector3.up;

    private void Start()
    {
        _animator.speed = _speedAnimator;
        _positionYOrigin = transform.position.y;
    }

    private void Update()
    {
        if (transform.position.y > _positionYOrigin + 50)
        {
            _direction = Vector3.down;
        }
        else if (transform.position.y < _positionYOrigin)
        {
            _direction = Vector3.up;
        }

        transform.Translate(_speedMove * Time.deltaTime * _direction);
    }
}
