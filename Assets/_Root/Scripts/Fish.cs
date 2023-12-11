using Fusion;
using UnityEngine;

public class Fish : NetworkBehaviour
{
    public Animator Animator;
    private bool _isCatched;
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

    public override void FixedUpdateNetwork()
    {
        if (_isCatched)
        {
            Animator.speed = 5;
        }
        else
        {
            Animator.speed = 1;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Runner.DeltaTime * 0.1f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.zero), Runner.DeltaTime * 500);

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
        _isCatched = true;
    }

    public void Released()
    {
        _isCatched = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            other.GetComponent<Hand>().SetFish(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isCatched && other.CompareTag("Hand"))
        {
            other.GetComponent<Hand>().SetFish(null);
        }
    }
}
