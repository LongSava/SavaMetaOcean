using UnityEngine;

public partial class Player
{
    private float _speedMove;

    public override void SpawnedServer()
    {
        _speedMove = Config.Data.Player.SpeedMove;
    }

    public override void FixedUpdateServer()
    {
        if (GetInput(out InputData input))
        {
            var speedAddition = 2 * Runner.DeltaTime * (input.TriggerButtonRight.IsSet(Buttons.TriggerButtonRight) ? 1 : -1);
            _speedMove = Mathf.Clamp(_speedMove + speedAddition, Config.Data.Player.SpeedMove, Config.Data.Player.SpeedMove * 3);

            var position = _speedMove * input.MoveBody * Runner.DeltaTime * transform.InverseTransformVector(_headDevice.forward);
            transform.Translate(position);

            var rotation = Config.Data.Player.SpeedRotate * input.RotateBody * Runner.DeltaTime * Vector3.up;
            transform.Rotate(rotation);

            HandleInput(input);

            _inputData = input;
        }

        if (_gyreLine != null)
        {
            transform.Translate(_gyreLine.transform.forward * Runner.DeltaTime * Config.Data.SpeedGyreLine, Space.World);
        }
    }
}
