using UnityEngine;

public class FishFlock : MonoBehaviour
{
    private Vector3 _targetPosition;

    private void Awake()
    {
        RandomTargetPosition();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * Config.Data.Flock.SpeedMove);
        if (transform.position == _targetPosition)
        {
            RandomTargetPosition();
        }
    }

    private void RandomTargetPosition()
    {
        var boxCollider = transform.parent.GetComponent<BoxCollider>();
        var xHalf = boxCollider.bounds.size.x / 2;
        var yHalf = boxCollider.bounds.size.y / 2;
        var zHalf = boxCollider.bounds.size.z / 2;
        _targetPosition = transform.parent.position + new Vector3(Random.Range(-xHalf, xHalf), Random.Range(-yHalf, yHalf), Random.Range(-zHalf, zHalf));
    }
}
