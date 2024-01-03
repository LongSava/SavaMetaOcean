using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishMaterial : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;
    [SerializeField] private List<SkinnedMeshRenderer> _skinnedMeshRenderers;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var material = _materials[Random.Range(0, _materials.Count - 1)];
            _skinnedMeshRenderers.ForEach(skinnedMeshRenderer => skinnedMeshRenderer.material = material);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _skinnedMeshRenderers.ForEach(skinnedMeshRenderer => skinnedMeshRenderer.material = _materials[_materials.Count - 1]);
        }
    }
}
