using UnityEngine;

public class FishFlock : MonoBehaviour
{
    private Vector3 _targetPosition;
    private FishArea _fishArea;

    private void Start()
    {
        RandomTargetPosition();
    }

    public void SetFishArea(FishArea fishArea)
    {
        _fishArea = fishArea;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * Config.Data.Fish.Flock.SpeedMove);
        if (transform.position == _targetPosition)
        {
            RandomTargetPosition();
        }
    }

    private void RandomTargetPosition()
    {
        var randomX = Random.Range(-_fishArea.XHalf, _fishArea.XHalf);
        var randomY = Random.Range(-_fishArea.YHalf, _fishArea.YHalf);
        var randomZ = Random.Range(-_fishArea.ZHalf, _fishArea.ZHalf);
        _targetPosition = transform.parent.position + new Vector3(randomX, randomY, randomZ);
    }
}
