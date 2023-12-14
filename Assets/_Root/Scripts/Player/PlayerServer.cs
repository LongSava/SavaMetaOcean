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

            var position = transform.InverseTransformVector(_headDevice.forward) * input.MoveBody * _speedMove * Runner.DeltaTime;
            transform.Translate(position);

            var rotation = Vector3.up * input.RotateBody * Config.Data.Player.SpeedRotate * Runner.DeltaTime;
            transform.Rotate(rotation);

            HandleInput(input);

            _inputData = input;
        }
    }
}
