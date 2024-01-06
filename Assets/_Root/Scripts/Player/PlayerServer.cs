using UnityEngine;

public partial class Player
{
    private float _speedMove;
    private Vector3 _direction;

    public override void FixedUpdateServer()
    {
        if (GetInput(out InputData input))
        {
            HandleInput(input);

            _inputData = input;
        }

        if (input.MoveBody != 0)
        {
            _direction = input.MoveBody * transform.forward;
            _speedMove += 15 * Runner.DeltaTime;
        }
        else
        {
            _speedMove -= 15 * Runner.DeltaTime;
        }
        _speedMove = Mathf.Clamp(_speedMove, 0, Config.Data.Player.SpeedMove);

        _rigidbody.MovePosition(transform.position + _speedMove * Runner.DeltaTime * _direction);

        var rotation = Config.Data.Player.SpeedRotate * input.RotateBody * Runner.DeltaTime * Vector3.up;
        transform.Rotate(rotation);

        if (_gyreLine != null)
        {
            transform.Translate(_gyreLine.transform.forward * Runner.DeltaTime * Config.Data.SpeedGyreLine, Space.World);
        }
    }
}
