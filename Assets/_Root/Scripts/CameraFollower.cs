using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;

    private void Update()
    {
        if (Target != null)
        {
            transform.SetPositionAndRotation(Target.position, Target.rotation);
        }
    }
}
