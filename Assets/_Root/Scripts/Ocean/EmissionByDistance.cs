using System;
using System.Collections;
using UnityEngine;

public class EmissionByDistance : MonoBehaviour
{
    [NonSerialized] public Renderer Renderer;
    [NonSerialized] public Player Player;

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        Renderer.material = new Material(Renderer.material);
        StartCoroutine(FindPlayer());
    }

    private IEnumerator FindPlayer()
    {
        while (Player == null)
        {
            if (Player.player != null)
            {
                Player = Player.player;
            }

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (Player == null) return;

        if (Renderer.isVisible)
        {
            var distance = Vector3.Distance(Player.transform.position, transform.position);
            var percent = Mathf.Clamp01(1 - distance / (Player.FlashLight.EnableLightFar ? Config.Data.Vision.Emission.Far : Config.Data.Vision.Emission.Near));
            Renderer.material.SetColor("_EmissionColor", new Color(percent, percent, percent));
        }
        else
        {
            Renderer.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
