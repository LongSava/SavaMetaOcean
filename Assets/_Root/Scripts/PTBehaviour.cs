using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PTBehaviour : NetworkBehaviour
{
    public override void FixedUpdateNetwork()
    {
        if (Runner.IsClient) FixedUpdateClient();
        if (Runner.IsServer) FixedUpdateServer();
    }
    public virtual void FixedUpdateClient() { }
    public virtual void FixedUpdateServer() { }

    public override void Spawned()
    {
        if (Runner.IsClient) SpawnedClient();
        if (Runner.IsServer) SpawnedServer();
    }
    public virtual void SpawnedClient() { }
    public virtual void SpawnedServer() { }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        if (runner.IsClient) DespawnedClient(runner, hasState);
        if (runner.IsServer) DespawnedServer(runner, hasState);
    }
    public virtual void DespawnedClient(NetworkRunner runner, bool hasState) { }
    public virtual void DespawnedServer(NetworkRunner runner, bool hasState) { }
}
