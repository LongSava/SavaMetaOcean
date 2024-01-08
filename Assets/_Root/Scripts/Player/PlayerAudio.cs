using Fusion;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public AudioSource Breathing;
    public AudioSource Swimming;
    public AudioSource SwimmingDown;

    public void ChangeSnapshotToBreathing()
    {
        AudioMixer.FindSnapshot("Breathing").TransitionTo(2);
    }

    public void ChangeSnapshotToSwimming()
    {
        AudioMixer.FindSnapshot("Swimming").TransitionTo(2);
    }

    public void ChangeSnapshotToSwimmingDown()
    {
        AudioMixer.FindSnapshot("SwimmingDown").TransitionTo(2);
    }

    public void RemoveAudio()
    {
        Destroy(Breathing.gameObject);
        Destroy(Swimming.gameObject);
        Destroy(SwimmingDown.gameObject);
    }
}
