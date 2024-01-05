using System.Collections;
using Fusion;
using UnityEngine;

public class Fish : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    private bool _isRelease = true;
    private FishFlock _flock;
    private Vector3 _offsetPosition;
    private float _speedMove;
    private Player _player;
    private Transform _fishTransform;

    public override void Spawned()
    {
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        var randomX = Random.Range(-Config.Data.Fish.RangeTargetPosition, Config.Data.Fish.RangeTargetPosition);
        var randomY = Random.Range(-Config.Data.Fish.RangeTargetPosition, Config.Data.Fish.RangeTargetPosition);
        var randomZ = Random.Range(-Config.Data.Fish.RangeTargetPosition, Config.Data.Fish.RangeTargetPosition);
        _offsetPosition = new Vector3(randomX, randomY, randomZ);

        _speedMove = Config.Data.Fish.SpeedMove + Random.Range(-Config.Data.Fish.RangeSpeedMove, Config.Data.Fish.RangeSpeedMove);

        yield return new WaitForSeconds(Random.Range(3, 5));
        StartCoroutine(Reset());
    }

    public void SetFlock(FishFlock fishFlock)
    {
        _flock = fishFlock;
    }

    public override void FixedUpdateNetwork()
    {
        if (_flock != null && _isRelease)
        {
            var position = transform.position + _speedMove * Runner.DeltaTime * transform.forward;

            Quaternion targetRotation;
            if (_player != null)
            {
                Vector3 direction = _player.transform.InverseTransformPoint(transform.position);
                targetRotation = Quaternion.LookRotation((direction.x > 0 ? 1 : -1) * 10 * _player.transform.right - transform.position);
            }
            else
            {
                targetRotation = Quaternion.LookRotation(_flock.transform.position + _offsetPosition - transform.position);
            }
            var rotation = Quaternion.Lerp(transform.rotation, targetRotation, Runner.DeltaTime * Config.Data.Fish.SpeedRotate);

            transform.SetPositionAndRotation(position, rotation);
        }
    }

    public override void Render()
    {
        if (!_isRelease)
        {
            transform.SetPositionAndRotation(_fishTransform.position, _fishTransform.rotation);
        }
    }

    public void Catched(Transform fishTransform)
    {
        _fishTransform = fishTransform;
        _isRelease = false;
        _animator.speed = Config.Data.Fish.SpeedMove * 5;
    }

    public void Released()
    {
        _isRelease = true;
        _animator.speed = Config.Data.Fish.SpeedMove;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
        }
    }
}
