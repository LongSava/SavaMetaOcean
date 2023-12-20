using System.Collections;
using UnityEngine;

public class JellyFishMaterial : MonoBehaviour
{
    [SerializeField] private Material _material;
    private float _speedChange;
    private Color _colorTarget;
    private Coroutine _coroutine;

    private IEnumerator ChangeColor()
    {
        var r = Random.value;
        var g = Random.value;
        var b = Random.value;
        var a = Mathf.Clamp(Random.value, 0.3f, 1f);
        _colorTarget = new Color(r, g, b, a);

        yield return new WaitForSeconds(_speedChange);

        StartCoroutine(ChangeColor());
    }

    private void Update()
    {
        _material.color = Color.Lerp(_material.color, _colorTarget, Time.deltaTime * _speedChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _coroutine = StartCoroutine(ChangeColor());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(_coroutine);
        }
    }
}
