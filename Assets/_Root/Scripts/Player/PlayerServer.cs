using UnityEngine;

public partial class Player
{
    private float _speedMove;
    private Vector3 _direction;

    public override void FixedUpdateServer()
    {
        if (GetInput(out InputData input))
        {
            _inputData = input;
        }

        if (_inputData.MoveBody != 0)
        {
            _direction = _inputData.MoveBody * _model.Head.forward;
            _speedMove += 15 * Runner.DeltaTime;
        }
        else
        {
            _speedMove -= 15 * Runner.DeltaTime;
        }
        _speedMove = Mathf.Clamp(_speedMove, 0, Config.Data.Player.SpeedMove);

        _rigidbody.MovePosition(transform.position + _speedMove * Runner.DeltaTime * _direction);

        var rotation = Config.Data.Player.SpeedRotate * _inputData.RotateBody * Runner.DeltaTime * Vector3.up;
        transform.Rotate(rotation);

        if (_gyreLine == null)
        {
            _rigidbody.MovePosition(transform.position + _speedMove * Runner.DeltaTime * _direction);
        }
        else
        {
            _rigidbody.MovePosition(transform.position + _speedMove * Runner.DeltaTime * _direction + _gyreLine.transform.forward * Runner.DeltaTime * Config.Data.SpeedGyreLine);
        }
    }
}
