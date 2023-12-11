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

            if (input.Move.y == 0)
            {
                Tread();
                SetWeightForChainIKHands(1);
            }
            else if (input.Move.y > 0)
            {
                Swim();
                SetWeightForChainIKHands(0);
            }
            else
            {
                Tread();
                SetWeightForChainIKHands(0);
            }

            _leftHand.SetGrapValue(input.GrapLeftValue.IsSet(Buttons.GrapLeft));
            _rightHand.SetGrapValue(input.GrapRightValue.IsSet(Buttons.GrapRight));
        }
    }
}
