using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ForestTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd; // �������� �� ��������� �����
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Hub(template)"); // �������� ����� � ������ �� ������!
    }
}
