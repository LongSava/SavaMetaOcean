using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField] private NetworkCharacterControllerPrototype _character;

    public override void FixedUpdateServer()
    {
        if (GetInput(out InputData input))
        {
            _character.Move(new Vector3(input.Move.x, 0, input.Move.y));
        }
    }
}
