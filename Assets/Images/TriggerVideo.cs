using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class QuestTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Ссылка на VideoPlayer
    public string sceneToLoad;       // Имя сцены для загрузки
    public IQuestPassCondition condition;  // Условие для квеста

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
        // Проверка, что игрок подошел к триггеру и квест завершен
        if (other.CompareTag("Player") && condition.isPassed() && !isQuestFinished)
        {
            isQuestFinished = true;  // Отметим, что квест завершен
            StartVideo();  // Запуск видео
        }
    }

    private void StartVideo()
    {
        if (videoPlayer != null)
        {
            Debug.Log("Starting video...");
            videoPlayer.Play();  // Запуск воспроизведения видео
            videoPlayer.loopPointReached += OnVideoEnd;  // Подписка на окончание видео
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
            SceneManager.LoadScene(sceneToLoad);  // Загружаем сцену
        }
        else
        {
            Debug.LogError("Scene name is not assigned!");
        }
    }
}
