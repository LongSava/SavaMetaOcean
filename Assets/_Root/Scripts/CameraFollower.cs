using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;

    private void Update()
    {
        if (Target != null)
        {
            transform.position = Target.position;
            transform.rotation = Target.rotation;
        }
    }
}
