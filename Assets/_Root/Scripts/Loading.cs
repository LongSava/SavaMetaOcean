using Fusion;
using UnityEngine;
using DG.Tweening;

public class Loading : NetworkBehaviour
{
    public Camera Camera;
    public CanvasGroup CanvasGroup;

    public override void Spawned()
    {
        if (Runner.IsServer)
        {
            GenerateEnvironmentServer();
        }
        else
        {
            Runner.GetComponent<EventScene>().SpawnedPlayer += GenerateEnvironmentClient;
        }
    }

    private void GenerateEnvironmentServer()
    {
        Runner.InstantiateInRunnerScene(Config.Data.Environment.Ocean);
        Hide();
    }

    private void GenerateEnvironmentClient()
    {
        Runner.InstantiateInRunnerScene(Config.Data.Environment.Ocean);
        Runner.InstantiateInRunnerScene(Config.Data.Environment.JellyFishs);
        Runner.InstantiateInRunnerScene(Config.Data.Environment.ClamShells);
        Runner.InstantiateInRunnerScene(Config.Data.Particle.BubbleCommon);
        Runner.InstantiateInRunnerScene(Config.Data.Particle.SunLight);
        Hide();
    }

    private void Hide()
    {
        CanvasGroup.DOFade(0, 2).OnComplete(() =>
        {
            Camera.enabled = false;
            CanvasGroup.interactable = false;
        });
    }
}
