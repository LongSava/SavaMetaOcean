using Fusion;
using UnityEngine;
using DG.Tweening;

public class Loading : NetworkBehaviour
{
    public CanvasGroup CanvasGroup;

    public override void Spawned()
    {
        if (Runner.IsClient)
        {
            Runner.GetComponent<EventScene>().Loaded += Hide;
        }
    }

    private void Hide()
    {
        CanvasGroup.DOFade(0, 1).OnComplete(() =>
        {
            CanvasGroup.interactable = false;
        });
    }
}
