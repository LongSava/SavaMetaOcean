using System.Collections;
using Fusion;
using UnityEngine;

public class Fish : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _isRelease = true;
    private FishFlock _flock;
    private Vector3 _offsetPosition;
    private float _speedMove;

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

    public override void Render()
    {
        if (_flock != null && _isRelease)
        {
            var targetRotation = Quaternion.LookRotation(_flock.transform.position + _offsetPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Runner.DeltaTime * Config.Data.Fish.SpeedRotate);

            transform.position += transform.forward * Runner.DeltaTime * _speedMove;
        }
    }

    public void Catched(Transform location)
    {
        transform.position = location.position;
        transform.rotation = location.rotation;
        _isRelease = false;
        _animator.speed = Config.Data.Fish.SpeedMove * 5;
    }

    public void Released()
    {
        _isRelease = true;
        _animator.speed = Config.Data.Fish.SpeedMove;
    }
}
