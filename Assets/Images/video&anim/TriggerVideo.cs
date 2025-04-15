using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class QuestTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // ������ �� VideoPlayer
    public string sceneToLoad;       // ��� ����� ��� ��������
    public IQuestPassCondition condition;  // ������� ��� ������

    private bool isQuestFinished = false;

    private void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer is not assigned in the inspector!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ��������, ��� ����� ������� � �������� � ����� ��������
        if (other.CompareTag("Player") && condition.isPassed() && !isQuestFinished)
        {
            isQuestFinished = true;  // �������, ��� ����� ��������
            StartVideo();  // ������ �����
        }
    }

    private void StartVideo()
    {
        if (videoPlayer != null)
        {
            Debug.Log("Starting video...");
            videoPlayer.Play();  // ������ ��������������� �����
            videoPlayer.loopPointReached += OnVideoEnd;  // �������� �� ��������� �����
        }
        else
        {
            Debug.LogError("VideoPlayer is not assigned or not set correctly!");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Video ended. Loading scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);  // ��������� �����
        }
        else
        {
            Debug.LogError("Scene name is not assigned!");
        }
    }
}
