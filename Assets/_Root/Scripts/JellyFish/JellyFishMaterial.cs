using System.Collections;
using UnityEngine;

public class JellyFishMaterial : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Vector2 _timeChange;
    private float _speedChange;
    private Color _colorTarget;

    private void Start()
    {
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        var r = Random.value;
        var g = Random.value;
        var b = Random.value;
        _colorTarget = new Color(r, g, b, 1);

        _speedChange = Random.Range(_timeChange.x, _timeChange.y);
        yield return new WaitForSeconds(_speedChange);

        StartCoroutine(ChangeColor());
    }

    private void Update()
    {
        _material.color = Color.Lerp(_material.color, _colorTarget, Time.deltaTime * _speedChange);
    }
}
