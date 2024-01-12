using System;
using System.Collections;
using UnityEngine;

public class EmissionByDistance : MonoBehaviour
{
    [SerializeField] public float MinClamp = 0;
    [SerializeField] public float MaxClamp = 1;
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
        if (Player == null || Player.FlashLight == null || !Player.HasInputAuthority) return;

        if (Renderer.isVisible)
        {
            var distance = Vector3.Distance(Player.transform.position, transform.position);
            var percent = 1 - distance / (Player.FlashLight.EnableLightFar ? Config.Data.Vision.Emission.Far : Config.Data.Vision.Emission.Near);
            var result = Mathf.Clamp(percent * percent, MinClamp, MaxClamp);
            Renderer.material.SetColor("_EmissionColor", new Color(result, result, result));
        }
    }
}
