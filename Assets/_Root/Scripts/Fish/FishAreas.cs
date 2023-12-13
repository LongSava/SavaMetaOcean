using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class FishAreas : NetworkBehaviour
{
    [SerializeField] private List<FishArea> _fishAreas;
    [SerializeField] private List<Fish> _fishes;

    public override void Spawned()
    {
        StartCoroutine(FindFishes());
    }

    private IEnumerator FindFishes()
    {
        yield return new WaitForSeconds(0.1f);

        var rootObjects = Runner.SimulationUnityScene.GetRootGameObjects();
        foreach (var rootObject in rootObjects)
        {
            var fishes = rootObject.GetComponentsInChildren<Fish>();
            foreach (var fish in fishes)
            {
                _fishes.Add(fish);
            }
        }

        _fishes.ForEach(fish => fish.transform.SetParent(transform));

        if (Runner.IsServer)
        {
            var index = 0;
            _fishAreas.ForEach(fishArea =>
            {
                for (int i = 0; i < fishArea.MaxFish; i++)
                {
                    if (index < _fishes.Count)
                    {
                        SetupFish(_fishes[index], fishArea);
                        index++;
                    }
                }
            });

            while (index < _fishes.Count)
            {
                SetupFish(_fishes[index], _fishAreas[0]);
                index++;
            }
        }
    }

    private void SetupFish(Fish fish, FishArea fishArea)
    {
        var fishFlock = fishArea.FishFlocks[Random.Range(0, fishArea.FishFlocks.Count)];
        fish.SetFlock(fishFlock);
        fish.transform.position = fishArea.transform.position;
    }
}
