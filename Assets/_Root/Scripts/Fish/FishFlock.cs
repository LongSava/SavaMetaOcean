using UnityEngine;

public class FishFlock : MonoBehaviour
{
    public FishFlockConfig Config;
    private FishArea _fishArea;
    private Transform _targetTransform;

    public void Init(FishFlockConfig config, FishArea fishArea)
    {
        Config = config;
        _fishArea = fishArea;

        transform.localPosition = Vector3.zero;

        _targetTransform = new GameObject("Target").transform;
        _targetTransform.SetParent(_fishArea.transform);

        RandomTargetPosition();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position, Time.fixedDeltaTime * Config.SpeedMove);
        if (transform.position == _targetTransform.position)
        {
            RandomTargetPosition();
        }
    }

    private void RandomTargetPosition()
    {
        var randomX = Random.Range(-_fishArea.XHalf, _fishArea.XHalf);
        var randomY = Random.Range(-_fishArea.YHalf, _fishArea.YHalf);
        var randomZ = Random.Range(-_fishArea.ZHalf, _fishArea.ZHalf);
        _targetTransform.position = _fishArea.transform.position + new Vector3(randomX, randomY, randomZ);

        _targetTransform.Rotate(_fishArea.transform.up, _fishArea.Config.Rotation.eulerAngles.y);
    }
}
