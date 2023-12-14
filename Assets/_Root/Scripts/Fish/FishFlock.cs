using UnityEngine;

public class FishFlock : MonoBehaviour
{
    public FishFlockConfig Config;
    private Vector3 _targetPosition;
    private FishArea _fishArea;

    public void Init(FishFlockConfig config, FishArea fishArea)
    {
        Config = config;
        _fishArea = fishArea;
        transform.localPosition = Vector3.zero;
        RandomTargetPosition();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * Config.SpeedMove);
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
