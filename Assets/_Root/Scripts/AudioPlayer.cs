using Fusion;
using UnityEngine;

public class AudioPlayer : NetworkBehaviour
{
    public override void Spawned()
    {
        if (Runner.IsClient)
        {
            Runner.GetComponent<EventScene>().SpawnedPlayer += (player) =>
            {
                transform.SetParent(player.HeadDevice);
                transform.localPosition = Vector3.zero;
            };
        }
    }
}
