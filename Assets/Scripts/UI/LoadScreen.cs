using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
 
    public GameObject _Screen;
    public Image LoadingBarFill;

    private void Start()
    {
        _Screen.SetActive(false);
        MainStaticData.loadScreen = this;
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        _Screen.SetActive(true);
        MainStaticData.OldSceen = SceneManager.GetActiveScene().buildIndex;

        if (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);

            LoadingBarFill.fillAmount = progressValue;
        }

        yield return null;

    }
}
