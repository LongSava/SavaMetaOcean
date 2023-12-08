using Fusion;
using UnityEngine;

public class Fish : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            other.GetComponent<Hand>().Fish = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            other.GetComponent<Hand>().Fish = null;
        }
    }
}
