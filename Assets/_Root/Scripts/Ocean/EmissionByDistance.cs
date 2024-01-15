using System;
using System.Collections;
using UnityEngine;

public class EmissionByDistance : MonoBehaviour
{
    [SerializeField] public float MinClamp = 0;
    [SerializeField] public float MaxClamp = 1;
    [NonSerialized] public Renderer Renderer;
    [NonSerialized] public Player Player;
    [NonSerialized] public float Intensity;
    public float IntensityCurrent = 5;

    private void Awake()
    {
        // Renderer = GetComponent<Renderer>();
        // Renderer.material = new Material(Renderer.material);
        // Intensity = Mathf.Pow(2, IntensityCurrent);
        // StartCoroutine(FindPlayer());
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
        if (Player == null || Player.FlashLight == null || !Player.HasInputAuthority) return;

        if (Renderer.isVisible)
        {
            var distance = Vector3.Distance(Player.transform.position, transform.position);
            var percent = 1 - distance / (Player.FlashLight.EnableLightFar ? Config.Data.Vision.Emission.Far : Config.Data.Vision.Emission.Near);
            var result = Mathf.Clamp(percent, MinClamp, MaxClamp) * Intensity;
            Renderer.material.SetColor("_EmissionColor", new Color(result, result, result, 1));
        }
    }
}
