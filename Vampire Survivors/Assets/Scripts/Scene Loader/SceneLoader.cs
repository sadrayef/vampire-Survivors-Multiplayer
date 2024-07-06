using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.Collections;


public class SceneLoader : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider loaddingBar;


     public void loadScene(int levelIndex)
    {
        /*
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        */
        StartCoroutine(LoadSceneAsyncronously(levelIndex));
    }

    IEnumerator LoadSceneAsyncronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            loaddingBar.value = operation.progress;
            yield return null; 
        }
    }

}
