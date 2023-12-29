using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public RoomType RoomType;
    public MeshRenderer Logo;
    public ParticleSystem Particle;
    public MeshRenderer Dark;

    private void Awake()
    {
        Logo.material.DOFade(0, 0);
        Dark.material.DOFade(0, 0);
    }

    private void Start()
    {
        if (Config.Data.NumberPlayer == NumberPlayer.Server)
        {
            SceneManager.LoadScene(RoomType.ToString());
        }
        else
        {
            DOTween.Sequence().AppendInterval(2).AppendCallback(() =>
            {
                Logo.material.DOFade(1, 3).OnComplete(() =>
                {
                    Particle.Play();
                    DOTween.Sequence().AppendInterval(1).AppendCallback(() =>
                    {
                        Dark.material.DOFade(1, 4);
                        Particle.transform.DOLocalMoveZ(0.4f, 4);
                        Logo.material.DOFade(0, 4);
                        Logo.transform.DOScale(30, 4).OnComplete(() =>
                        {
                            Destroy(Particle.gameObject);
                            DOTween.Sequence().AppendInterval(1).AppendCallback(() =>
                            {
                                SceneManager.LoadScene(RoomType.ToString());
                            });
                        });
                    });
                });
            });
        }
    }
}
