using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyreLine : MonoBehaviour
{
    public int Index;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player.GyreLine != null)
            {
                if (player.GyreLine.Index > Index || player.GyreLine.Index == 0)
                {
                    player.GyreLine = this;
                }
            }
            else
            {
                player.GyreLine = this;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().GyreLine = null;
        }
    }
}
