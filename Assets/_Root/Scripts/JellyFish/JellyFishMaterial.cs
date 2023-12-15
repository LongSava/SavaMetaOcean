using System.Collections;
using UnityEngine;

public class JellyFishMaterial : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Vector2 _timeChange;
    private float _speedChange;
    private Color _colorTarget;
    private bool _isPlayerStay;

    private void Start()
    {
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        var r = Random.value;
        var g = Random.value;
        var b = Random.value;
        var a = Mathf.Clamp(Random.value, 0.3f, 1f);
        _colorTarget = new Color(r, g, b, a);

        if (_isPlayerStay)
        {
            _speedChange = 0.5f;
        }
        else
        {
            _speedChange = Random.Range(_timeChange.x, _timeChange.y);
        }
        yield return new WaitForSeconds(_speedChange);

        StartCoroutine(ChangeColor());
    }

    private void Update()
    {
        _material.color = Color.Lerp(_material.color, _colorTarget, Time.deltaTime * _speedChange);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerStay = false;
        }
    }
}
