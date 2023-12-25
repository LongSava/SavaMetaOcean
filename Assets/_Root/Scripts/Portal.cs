using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public RoomType RoomType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            player.DisableEyes(() =>
            {
                switch (RoomType)
                {
                    case RoomType.Ocean:
                        RunnerController.Instance.Destroy();
                        SceneManager.LoadScene("Ocean");
                        break;
                    case RoomType.Titanic:
                        RunnerController.Instance.Destroy();
                        SceneManager.LoadScene("Titanic");
                        break;
                }
            });
        }
    }
}
