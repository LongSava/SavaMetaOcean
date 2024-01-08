using UnityEngine;

public class Portal : MonoBehaviour
{
    public RoomType RoomType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<Player>();

            if (player.HasInputAuthority)
            {
                player.DisableEyes(() =>
                {
                    switch (RoomType)
                    {
                        case RoomType.Ocean:
                            RunnerController.Instance.Destroy();
                            SceneController.Instance.LoadScene(SceneType.Ocean);
                            break;
                        case RoomType.Titanic:
                            RunnerController.Instance.Destroy();
                            SceneController.Instance.LoadScene(SceneType.Titanic);
                            break;
                    }
                });
            }
        }
    }
}
