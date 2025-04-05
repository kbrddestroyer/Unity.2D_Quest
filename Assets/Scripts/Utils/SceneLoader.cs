using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] protected string sceneName;
    
    protected IEnumerator asyncInvoke()
    {
        AsyncOperation opLoad = SceneManager.LoadSceneAsync(sceneName);
    
        while (!opLoad.isDone)
        {
            yield return null;
        }
    }

    public virtual void invoke()
    {
        StartCoroutine(asyncInvoke());
    }
}
