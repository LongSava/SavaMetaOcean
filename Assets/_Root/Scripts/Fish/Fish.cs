using Fusion;
using UnityEngine;

public class Fish : NetworkBehaviour
{
    public Animator Animator;
    private bool _isRelease = true;
    private Vector3 _targetPosition;

    public override void Spawned()
    {
        RandomTargetPosition();
    }

    private void RandomTargetPosition()
    {
        var randomX = Random.Range(-5, 5);
        var randomY = Random.Range(-5, 5);
        var randomZ = Random.Range(-5, 5);
        _targetPosition = transform.position + new Vector3(randomX, randomY, randomZ);
    }

    public override void Render()
    {
        if (_isRelease)
        {
            var targetRotation = Quaternion.LookRotation(_targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Runner.DeltaTime * Config.Data.Fish.SpeedRotate);

            transform.position += transform.forward * Runner.DeltaTime * Config.Data.Fish.SpeedMove;

            if (transform.position == _targetPosition)
            {
                RandomTargetPosition();
            }
        }
    }

    public void Catched(Transform location)
    {
        transform.position = location.position;
        transform.rotation = location.rotation;
        _isRelease = false;
        Animator.speed = 5;
    }

    public void Released()
    {
        _isRelease = true;
        Animator.speed = 1;
    }
}
