using UnityEngine;

public partial class Player
{
    public override void FixedUpdateServer()
    {
        if (GetInput(out InputData input))
        {
            var position = transform.InverseTransformVector(_headDevice.forward) * input.MoveBody * Config.Data.Player.SpeedMove * Runner.DeltaTime;
            transform.Translate(position);

            var rotation = Vector3.up * input.RotateBody * Config.Data.Player.SpeedRotate * Runner.DeltaTime;
            transform.Rotate(rotation);
        }
    }
}
