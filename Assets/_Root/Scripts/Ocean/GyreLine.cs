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
            var player = other.GetComponentInParent<Player>();
            if (player.GyreLine != null)
            {
                if (player.GyreLine.Index < Index)
                {
                    player.GyreLine = this;
                }
                else if (player.GyreLine.Index == 0)
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
            other.GetComponentInParent<Player>().GyreLine = null;
        }
    }
}
