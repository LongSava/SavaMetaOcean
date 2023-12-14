using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class FishAreas : NetworkBehaviour
{
    private List<FishArea> _fishAreas = new List<FishArea>();
    private List<Fish> _fishes = new List<Fish>();

    public override void Spawned()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return new WaitForEndOfFrame();

        FindFishes();
        SetupAreas();
    }

    private void FindFishes()
    {
        var rootObjects = Runner.SimulationUnityScene.GetRootGameObjects();
        foreach (var rootObject in rootObjects)
        {
            var fishes = rootObject.GetComponentsInChildren<Fish>();
            foreach (var fish in fishes)
            {
                _fishes.Add(fish);
            }
        }

        var fishesObject = Runner.InstantiateInRunnerScene(new GameObject("Fishes"));
        fishesObject.transform.SetParent(transform);
        _fishes.ForEach(fish => fish.transform.SetParent(fishesObject.transform));
    }

    private void SetupAreas()
    {
        if (Runner.IsServer)
        {
            Config.Data.FishAreas.FishAreas.ForEach(config =>
            {
                var fishArea = Runner.InstantiateInRunnerScene(new GameObject("FishArea")).AddComponent<FishArea>();
                fishArea.transform.SetParent(transform);
                fishArea.Init(config, Runner);
                _fishAreas.Add(fishArea);
            });

            var index = 0;
            _fishAreas.ForEach(fishArea =>
            {
                for (int i = 0; i < fishArea.Config.NumberFish; i++)
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
