using System.Collections;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public ParticleSystem Bubbles;
    public AudioSource BubblesSound;

    private void Start()
    {
        StartCoroutine(PlayBubbles());
    }

    public IEnumerator PlayBubbles()
    {
        yield return new WaitForSeconds(Random.Range(5, 20));
        Bubbles.Play();
        BubblesSound.Play();
        StartCoroutine(PlayBubbles());
    }
}
