using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ForestTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    [Tooltip("Название сцены для загрузки после окончания видео")]
    public string sceneToLoad;  // Поле для имени сцены, которое будет указано в инспекторе Unity

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd; // Подписка на окончание видео
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Проверяем, что название сцены не пустое
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad); // Загружаем сцену с именем, указанным в инспекторе
        }
        else
        {
            Debug.LogWarning("Scene name is not assigned!");
        }
    }
}
