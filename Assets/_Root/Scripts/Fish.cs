using Fusion;
using UnityEngine;

public class Fish : NetworkBehaviour
{
    public Animator Animator;
    private bool isCatched;

    private void Update()
    {
        if (isCatched)
        {
            Animator.speed = 5;
        }
        else
        {
            Animator.speed = 1;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * 500);
        }
    }

    public void Catched(Transform location)
    {
        transform.position = location.position;
        transform.rotation = location.rotation;
        isCatched = true;
    }

    public void Released()
    {
        isCatched = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            other.GetComponent<Hand>().SetFish(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            other.GetComponent<Hand>().SetFish(null);
        }
    }
}
