using Fusion;

public class FishGroup : NetworkBehaviour
{
    public override void Spawned()
    {
        var rootObjects = Runner.SimulationUnityScene.GetRootGameObjects();
        foreach (var rootObject in rootObjects)
        {
            var fishes = rootObject.GetComponentsInChildren<Fish>();
            foreach (var fish in fishes)
            {
                fish.transform.SetParent(transform);
            }
        }
    }
}
