using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Fish Fish;
    public float GrapValue;

    public void Grap(float grapValue)
    {
        GrapValue = grapValue;
    }

    private void Update()
    {
        if (Fish != null && GrapValue == 1)
        {
            Fish.transform.position = transform.position;
        }
    }
}
