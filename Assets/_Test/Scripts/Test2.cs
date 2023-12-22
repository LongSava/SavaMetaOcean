using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEditor;

public class Test2 : MonoBehaviour
{
    public SplineComputer SplineComputer;
    public GameObject GyreLine;

    public void Calculate()
    {
        var points = SplineComputer.GetPoints();
        for (int i = 0; i < points.Length; i++)
        {
            if (i == points.Length - 1)
            {
                GenerateGyreLine(points[points.Length - 1].position, points[0].position);
            }
            else
            {
                GenerateGyreLine(points[i + 1].position, points[i].position);
            }
        }
    }

    private void GenerateGyreLine(Vector3 startPoint, Vector3 endPoint)
    {
        var gyreLine = Instantiate(GyreLine, transform);
        gyreLine.transform.localScale = new Vector3(1, 1, Vector3.Distance(startPoint, endPoint));
        gyreLine.transform.position = (endPoint + startPoint) / 2;
        gyreLine.transform.Rotate(gyreLine.transform.up, Vector3.Angle(gyreLine.transform.forward, endPoint - startPoint));
    }
}

[CustomEditor(typeof(Test2))]
public class Test2Editor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Cal"))
        {
            ((Test2)target).Calculate();
        }
    }
}