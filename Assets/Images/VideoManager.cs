using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ForestTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    [Tooltip("�������� ����� ��� �������� ����� ��������� �����")]
    public string sceneToLoad;  // ���� ��� ����� �����, ������� ����� ������� � ���������� Unity

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
        // ���������, ��� �������� ����� �� ������
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad); // ��������� ����� � ������, ��������� � ����������
        }
        else
        {
            Debug.LogWarning("Scene name is not assigned!");
        }
    }
}
