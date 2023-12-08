using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public override void FixedUpdateServer()
    {
        if (GetInput(out InputData input))
        {
            var position = new Vector3(0, 0, input.Move.y * Config.Data.SpeedMove) * Runner.DeltaTime;
            transform.Translate(position);

            var rotation = new Vector3(0, input.Move.x * Config.Data.SpeedRotate, 0) * Runner.DeltaTime;
            transform.Rotate(rotation);

            LeftHand.Grap(input.GrapLeft);
            RightHand.Grap(input.GrapRight);

            CheckMove(input);
        }
    }

    private void CheckMove(InputData input)
    {
        if (input.Move.y > 0) Swim();
        else Tread();
    }
}
