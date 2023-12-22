using UnityEngine;
using Dreamteck.Splines;

public class Gyre : MonoBehaviour
{
    public SplineComputer SplineComputer;
    public GyreLine GyreLine;
    private int _index;

    public void Start()
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
        gyreLine.transform.Rotate(gyreLine.transform.up, -Vector3.SignedAngle(gyreLine.transform.forward, endPoint - startPoint, gyreLine.transform.up));
        gyreLine.gameObject.SetActive(true);
        gyreLine.Index = _index++;
    }
}